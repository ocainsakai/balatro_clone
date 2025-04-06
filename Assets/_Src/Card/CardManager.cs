using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Poker;
using System;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] List<StandardCardSO> deck;
        [SerializeField] CardFactory cardFactory;
        [SerializeField] PokerHandRuntime pokerHand;
        [SerializeField] GameStateEvent gameStateEvent;
        public List<StandardCard> hand = new List<StandardCard>();
        public List<IStandardCard> playingdeck;
        public List<IStandardCard> discardPile = new List<IStandardCard>();

        [SerializeField] float cardSpace = 1f;

        public Action currentSort;
        public int HandCount => hand.Count;
        public int handSize = 8;
        public int SelectedCount => hand.Where(x => x.isSelected).Count();
        public bool canShuffe = false;
        private void Start()
        {
            currentSort = SortByRank;
            playingdeck = deck.Select(x => x.data).ToList();
            ShuffleDeck();
        }

        #region DrawPhase
        public void Draw()
        {
            int amount = handSize - hand.Count;
            for (int i = 0; i < amount; i++)
            {
                var cardPrf = cardFactory.CreatCard();
                var card = DrawCard();
                hand.Add(cardPrf);
                cardPrf.SetInit(card, this);
            }
            currentSort?.Invoke();
        }
        public IStandardCard DrawCard()
        {
            if (playingdeck.Count <= 0)
            {
                RestoreFormDiscardPile();
            }
            IStandardCard card = playingdeck[0];
            playingdeck.RemoveAt(0);
            return card;
        }
        private void RestoreFormDiscardPile()
        {
            playingdeck.AddRange(discardPile);
            discardPile.Clear();
            ShuffleDeck();
        }
        #endregion

        #region DiscardPhase
        public void RemoveSelectedCards()
        {
            for (int i = HandCount - 1; i >= 0; i--)
            {
                var card = hand[i];
                if (card.isSelected)
                {
                    hand.Remove(card);
                    discardPile.Add(card);
                    card.OnRemove();
                }
            }
            Draw();
        }
        #endregion

        #region PlayPhase
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
            gameStateEvent.Raise(Core.GameState.Decide);
        }
        #endregion
        public List<IStandardCard> GetSelectedCards()
        {
            var selected = hand.Where(x => x.isSelected).Select(x => x.data).ToList();
            return selected;
        }
        public void SortByRank()
        {
            currentSort = SortByRank;
            hand = hand.OrderBy(x => x.Rank).ThenBy(x => x.Suit).ToList();
            UpdatePositionCard();
        }
        public void SortBySuit()
        {
            currentSort = SortBySuit;

            hand = hand.OrderBy(x => x.Suit).ThenBy(x => x.Rank).ToList();
            UpdatePositionCard();
        }
        public void UpdatePositionCard()
        {
            for (int i = 0; i < hand.Count; i++)
            {
                var target = cardFactory.transform.position + Vector3.right * i * cardSpace;
                hand[i].transform.DOMove(target, 0.3f).SetEase(Ease.OutQuad);
                hand[i].OnUnselect();
            }
        }
        public void ShuffleDeck()
        {
            if (!canShuffe) return;
            for (int i = playingdeck.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                (playingdeck[i], playingdeck[randomIndex]) = (playingdeck[randomIndex], playingdeck[i]);
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
        public void ClearHand()
        {
            for (int i = HandCount - 1; i >= 0; i--)
            {
                var card = hand[i]; 
                hand.Remove(card);
                discardPile.Add(card);
                card.OnRemove();
            }
            UpdatePokerHand();
            RestoreFormDiscardPile();
        }
    }

}
