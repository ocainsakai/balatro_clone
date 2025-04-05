using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Poker;

namespace Card
{
    public class CardContainer : MonoBehaviour
    {
        public List<StandardCard> hand = new List<StandardCard>();
        [SerializeField] PokerHandRuntime pokerHand;
        [SerializeField] float cardSpace = 1f;
        [SerializeField] StandardCardEvent onDiscard;
        [SerializeField] GameEvent onDiscardComplete;
        public int HandCount => hand.Count;
        public int SelectedCount => hand.Where(x => x.isSelected).Count();
        public void AddHand(IEnumerable<StandardCard> hand)
        {
            foreach (var item in hand)
            {
                AddHand(item);
            }
            UpdatePositionCard();
        }
        public void AddHand(StandardCard card)
        {
            hand.Add(card);
            card.transform.SetParent(transform, false);
            UpdatePositionCard();
        }
        public void RemoveSelectedCards()
        {
            for (int i = HandCount - 1; i >= 0; i--)
            {
                var card = hand[i];
                if (card.isSelected)
                {
                    hand.Remove(card);
                    onDiscard.Raise(card);
                    card.OnRemove();
                }
            }
            onDiscardComplete.Raise();
        }
        public void CalculateSelected()
        {
            var cards = GetSelectedCards();
            var result = PokerEvaluator.Evaluator(cards);

            for (int i = 0; i < result.ValidCards.Count; i++)
            {
                var card = result.ValidCards[i];
                hand.FirstOrDefault(x => x.data == card).OnCalculate();
                pokerHand.Chip += card.Value;
            }
            pokerHand.AddScore();
            RemoveSelectedCards();
        }
        public List<IStandardCard> GetSelectedCards()
        {
            var selected = hand.Where(x => x.isSelected).Select(x => x.data).ToList();
            return selected;
        }
        public void SortByRank()
        {
            hand = hand.OrderBy(x => x.Rank).ThenBy(x => x.Suit).ToList();
            UpdatePositionCard();
        }
        public void SortBySuit()
        {
            hand = hand.OrderBy(x => x.Suit).ThenBy(x => x.Rank).ToList();
            UpdatePositionCard();
        }
        public void UpdatePositionCard()
        {
            for (int i = 0; i < hand.Count; i++)
            {
                var target = transform.position + Vector3.right * i * cardSpace;
                hand[i].transform.DOMove(target, 0.3f).SetEase(Ease.OutQuad);
                hand[i].OnUnselect();
            }
        }
        public void UpdatePokerHand()
        {
            var result = PokerEvaluator.Evaluator(GetSelectedCards());
            if (result != null)
            {
                pokerHand.data = result.PokerHand;
            } else
            {
                pokerHand.Reset();
            }
        }
    }

}
