using Balatro.Core;
using Balatro.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Balatro.Cards
{
    public class HandManager : MonoBehaviour
    {
        [SerializeField] private CardFactory _factory;
        [SerializeField] UIPokerHand _uiHand;
        [SerializeField] UIRoundScore _uiScore;
        //[SerializeField] private CardsAnimator _anim;
        [SerializeField] private Transform handTrf;
        [SerializeField] private Transform deckTrf;
        private event Action CurrentSort;
        private List<CardView> _currentHand = new List<CardView>();
        public int MAX_HAND_SIZE = 8;
        public int hand_count => _currentHand.Count;
        public int score => _uiScore._totalScore;
        public int selected_count => _currentHand.Where(x => x.isSelect == true).Count();
        public float _cardSpacing = 10f;
        public float _cardWidth = 50f;


        private void Start()
        {
            CurrentSort = SortHandByRank;
        }
        public void AddCardToHand(CardDataSO card)
        {
            if (_currentHand.Count < MAX_HAND_SIZE)
            {
                CardView cardView = _factory.CreateCard(card);
                cardView.CardViewClicked += OnCardClick;
                _currentHand.Add(cardView);
            }
            else
            {
                Debug.LogWarning("Hand is full. Cannot add more cards.");
            }
        }

        private void OnCardClick(CardView obj)
        {
            if (obj.isSelect)
            {
                obj.UnSelectCard(0.2f);
                UpdateUIHand();
            } else if (selected_count < 5 && !obj.isSelect)
            {
                obj.SelectCard(0.2f);
                UpdateUIHand();

            }
            //if (hand_count == 0) return;
        }
        public void UpdateUIHand()
        {
            var hand = _currentHand.Where(x => x.isSelect).Select(x => x.currentCardData).ToList();
            var result = PokerHandEvaluator.EvaluateHand(hand);
            _uiHand.UpdatePokerHand(result.HandType);
        }
        public IEnumerator CalculateSelected()
        {
            var hand = _currentHand.Where(x => x.isSelect).Select(x => x.currentCardData).ToList();
            var result = PokerHandEvaluator.EvaluateHand(hand);
            
            foreach (var item in _currentHand)
            {
                if (item.isSelect && result.ValidCards.Contains(item.currentCardData))
                {
                    _uiHand.UpdateChip(item.currentCardData.value);
                    yield return StartCoroutine(item.ShowValue());
                }
            }
            int score = (int) (_uiHand.chip * _uiHand.mul);
            _uiScore.PlusScore(score);
            if (_uiScore._totalScore >= GameManager.instance.blindScore)
            {
                GameManager.instance.StartSelectPhase();
            }
            yield return RemoveSelected();
        }
        public IEnumerator RemoveSelected()
        {
            foreach (var item in _currentHand)
            {
                if (item.isSelect)
                {
                    item.FlipCard();
                    StartCoroutine(item.MoveToPos(deckTrf.position, 0.2f));
                }
            }
                    yield return new WaitForSeconds(0.2f);
            
            _currentHand.RemoveAll(x => x.isSelect);
            UpdateUIHand();
        }

        // Sắp xếp bài theo giá trị
        public void SortHandByRank()
        {
            CurrentSort = SortHandByRank;
            _currentHand = _currentHand.OrderBy(c => c.currentCardData.rank).ToList();
            SortProcess();
        }

        // Sắp xếp bài theo chất
        public void SortHandBySuit()
        {
            CurrentSort = SortHandBySuit;
            _currentHand = _currentHand.OrderBy(c => c.currentCardData.suit).ToList();
            SortProcess();
        }
        public void Sort()
        {
            CurrentSort?.Invoke();
        }
        private void SortProcess()
        {
            //CurrentSort?.Invoke();

            for (int i = 0; i < _currentHand.Count; i++)
            {
                CardView card = _currentHand[i];
                card.isSelect = false;
                Vector3 target = handTrf.position + Vector3.right * i * (_cardSpacing + _cardWidth);
                card.gameObject.SetActive(true);
                StartCoroutine(card.MoveToPos(target, 0.2f));
            }
        }
        public List<CardView> GetCurrentHand() => _currentHand;
    }

}
