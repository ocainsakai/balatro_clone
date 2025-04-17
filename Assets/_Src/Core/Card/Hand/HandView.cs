
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Balatro.Cards.UI
{
    public class HandView : MonoBehaviour
    {

        [SerializeField] float cardSpace = 1.5f;
        public Transform cardContainer;
        public List<Card> handCards;
        SortType sortType;
        public void Sort(List<Card> handCards)
        {
            this.handCards = handCards; 
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
            handCards = handCards.OrderBy(x => x.data.Rank).ThenBy(x => x.data.Suit).ToList();
            UpdateCardsPosition();
        }
        public void SortBySuit()
        {
            sortType = SortType.BySuit;

            handCards = handCards.OrderBy(x => x.data.Suit).ThenBy(x => x.data.Rank).ToList();
            UpdateCardsPosition();
        }
        public void UpdateCardsPosition()
        {
            int size = handCards.Count;
            for (int i = 0; i < size; i++)
            {
                var target = cardContainer.transform.position + Vector3.right * (i - size/2) * cardSpace;
                var card  = handCards[i];
                card.transform.DOMove(target, 0.3f).SetEase(Ease.OutQuad);
                card.CardView.Deselect();

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

