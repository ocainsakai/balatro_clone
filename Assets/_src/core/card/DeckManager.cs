using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
namespace Balatro.Cards
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private CardFactory _factory;
        [SerializeField] private CardAnimator _anim;

        [SerializeField] private List<CardDataSO> card_pool 
            => Resources.LoadAll<CardDataSO>(typeof(CardDataSO).Name).ToList();
        //[SerializeField] private CardFactory _factory;
        //private List<CardView> _createdCards = new List<CardView>();
        public List<Card> deck = new List<Card>();
        public List<Card> hand = new List<Card>();
        public List<Card> discardPile = new List<Card>();

        public event Action<PokerHandType> OnPokerChange;
        public event Action<int> AddChip;
        public event Action<int> AddMul;
        public event Action OnScoreChange;
        public event Action NextRound;
        private List<Card> selectedCards => hand.Where(x => x.isSelect).ToList();
        public bool isSortBySuit = true;
        public int hand_size = 8;
      
        public void ResetDeck()
        {
            deck.Clear();
            discardPile.Clear();
            hand.Clear();
            _factory.ClearAllCards();
        }
        public void StartPhase()
        {
            foreach (var item in card_pool)
            {
                Card card = _factory.CreateCard(item);
                card.OnCardClick += ClickOnCard;
                card.gameObject.SetActive(false);
                deck.Add(card);
            }
            Shuffle();
            AddCardToHand();
        }
        private PokerHandResult EvaluatorHand()
        {
            var result =
            PokerHandEvaluator.EvaluateHand(selectedCards.Select(x => x.data).ToList());
            OnPokerChange?.Invoke(result.HandType);
            return result;
        }
        private void ClickOnCard(Card card)
        {
            if (card.isSelect)
            {
                card.isSelect = false;
                card.UnSelectCard(0.2f);
                EvaluatorHand();
            } else if (hand.Where(x => x.isSelect).Count() < 5)
            {
                card.isSelect = true;
                card.SelectCard(0.2f);
                EvaluatorHand();

            }
        }

        public void Shuffle()
        {
            System.Random random = new System.Random();
            deck = deck.OrderBy(x => random.Next()).ToList();
        }
        public Card DrawCard()
        {
            if (deck.Count <= 0)
            {
                RestoreDeckFromDiscardPile();
            }

            Card drawnCard = deck[0];
            deck.RemoveAt(0);
            return drawnCard;
            
        }
        public List<Card> DrawCards(int count)
        {
            List<Card> drawnCards = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                drawnCards.Add(DrawCard());
            }
            return drawnCards;
        }
        public void AddCardToHand()
        {
            hand.AddRange(DrawCards(hand_size - hand.Count));
            Sort();
        }
        public void PlayHand()
        {
            NextRound?.Invoke();
            var result = EvaluatorHand().ValidCards;
            var validCards = EvaluatorHand().ValidCards;
            var cards = hand.Where(x => validCards.Contains(x.data)).ToList();

            StartCoroutine(_anim.CalculateCard(cards, AddChip, AddMul, OnScoreChange));
        }
        
        public void DiscardAndDraw()
        {
            var cards = hand.Where(x => x.isSelect == true).ToList();
            discardPile.AddRange(cards);
            deck.RemoveAll(x => cards.Contains(x));
            hand.RemoveAll(x => cards.Contains(x));
            StartCoroutine(_anim.MoveCardsToDeck(cards));
            AddCardToHand();
            //StartCoroutine(DiscardProcess(cards));
        }
        //private IEnumerator DiscardProcess(List<Card> cards)
        //{
        //    yield return (_anim.MoveCardsToDeck(cards));
        //    AddCardToHand();
        //}
        public void Sort()
        {
            if (isSortBySuit)
            {
                hand = hand.OrderBy(x => x.data.suit).ThenBy(x => x.data.rank).ToList();
            }
            else
            {
                hand = hand.OrderBy(x => x.data.rank).ThenBy(x => x.data.suit).ToList();
            }
            StartCoroutine(_anim.FillToHand(hand));
        }
        public void Sort(bool isBySuit)
        {
            isSortBySuit = isBySuit;
            Sort();
        }
        private void RestoreDeckFromDiscardPile()
        {
            deck.AddRange(discardPile);
            discardPile.Clear();
            Shuffle();
        }
        public int RemainingCardCount => deck.Count;
    }
}
