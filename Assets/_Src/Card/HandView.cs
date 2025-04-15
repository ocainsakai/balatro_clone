using Balatro.Cards.CardsRuntime;
using Balatro.Cards.System;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

namespace Balatro.Cards.UI
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] CardFactory cardFactory;
        [SerializeField] Transform _cardContainer;
        [SerializeField] HandData handData;
        [SerializeField] CardDataEvent cardSelectedEvent;
        [SerializeField] float cardSpace = 1.5f;
        [SerializeField] List<Card> cardList = new List<Card>();
        int selectedCount = 0;
        SortType sortType;

        private void Awake()
        {
            handData.OnUpdated += HandData_OnUpdated;
            handData.OnAdded += HandData_OnAdded;
            handData.OnRemoved += HandData_OnRemoved;
        }
        public void OnCardSelect(CardView card)
        {
            var data = cardList.Find(x => x.CardView == card).data;  
            if (card.isSelected)
            {
                card.OnUnseleted();
                selectedCount--;
                cardSelectedEvent.Raise(data);
                
            } else if (selectedCount < 5)
            {
                card.OnSeleted();
                selectedCount++;
                cardSelectedEvent.Raise(data);

            }
        }
        private void HandData_OnRemoved(CardData obj)
        {
            
        }

        private void HandData_OnAdded(CardData obj)
        {
     
            if (obj != null)
            {
                var card = cardFactory.CreateCard(obj, _cardContainer);
                cardList.Add(card);
                card.CardView.OnSelected += OnCardSelect;
            }

        }

        private void HandData_OnUpdated()
        {
            Sort();
        }
        public void Sort()
        {
            switch (sortType)
            {
                case SortType.ByRank:
                    SortByRank();
                    break;
                case SortType.BySuit: 
                    SortBySuit(); 
                    break;
                default:
                    UpdateCardsPosition();
                    break;
            }
        }
        public void SortByRank()
        {
            sortType = SortType.ByRank;
            var cards = cardList.OrderBy(x => x.data.Rank).ThenBy(x => x.data.Suit);
            UpdateCardsPosition();
        }
        public void SortBySuit()
        {
            sortType = SortType.BySuit;

            var cards = cardList.OrderBy(x => x.data.Suit).ThenBy(x => x.data.Rank);
            UpdateCardsPosition();
        }
        public void UpdateCardsPosition()
        {
            
            for (int i = 0; i < cardList.Count; i++)
            {
                var target = cardFactory.transform.position + Vector3.right * i * cardSpace;
                cardList[i].transform.DOMove(target, 0.3f).SetEase(Ease.OutQuad);
                //hand[i].OnUnselect();
            }
        }
    }
    public enum SortType
    {
        None,
        ByRank,
        BySuit,
    }
}

