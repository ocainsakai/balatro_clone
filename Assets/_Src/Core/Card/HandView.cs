
using Balatro.Cards.CardsRuntime;
using DG.Tweening;
using System.Linq;
using UnityEngine;


namespace Balatro.Cards.UI
{
    public class HandView : MonoBehaviour
    {

        [SerializeField] Transform _cardContainer;
        [Header("RSO")]
        [SerializeField] CardsRSO handCards;
        [SerializeField] CardsRSO selectedCards;
        [SerializeField] float cardSpace = 1.5f;
        SortType sortType;

        private void Awake()
        {
            //handData.OnListChanged += HandData_OnUpdated;
            handCards.OnAdded += OnAdded;
            handCards.OnRemoved += OnRemoved;
        }

        private void OnAdded(Card card)
        {
            Sort();
        }

        private void OnRemoved(Card card)
        {
            card.CardView.Remove();
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
            handCards.List = handCards.List.OrderBy(x => x.data.Rank).ThenBy(x => x.data.Suit).ToList();
            UpdateCardsPosition();
        }
        public void SortBySuit()
        {
            sortType = SortType.BySuit;

            handCards.List = handCards.List.OrderBy(x => x.data.Suit).ThenBy(x => x.data.Rank).ToList();
            UpdateCardsPosition();
        }
        public void UpdateCardsPosition()
        {
            int size = handCards.List.Count;
            for (int i = 0; i < size; i++)
            {
                var target = _cardContainer.transform.position + Vector3.right * (i - size/2) * cardSpace;
                var card  = handCards.List[i];
                card.transform.DOMove(target, 0.3f).SetEase(Ease.OutQuad);
                card.CardView.Deselect();
                selectedCards.RemoveCard(card);

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

