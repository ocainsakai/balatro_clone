using Balatro.Cards.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Cards.System
{
    public class HandManager : BaseManager
    {
        [SerializeField] CardFactory cardFactory;
        [SerializeField] HandView handView;

        public event Action OnSelect;

        public List<Card> handCards;
        public int HandSzie = 8;
        public int HandCount => handCards.Count;
        public int SelectCount => GetSelected().Count;

        public override void Initialize()
        {
            base.Initialize();
        }
        public List<Card> GetSelected()
        {
            return handCards.Where(x => x.CardView.isSelected).ToList();
        }
        public void OnCardSelect(CardView card)
        {
            if (card.isSelected)
            {
                card.Deselect();
                OnSelect?.Invoke();
            }
            else if (SelectCount < 5)
            {
                card.Select();
                OnSelect?.Invoke();

            }
        }
        public void Draw()
        {
            while (HandCount < HandSzie)
            {
                var cardData = GetManager<DeckManager>().Draw();
                var card = cardFactory.CreateCard(cardData, OnCardSelect);
                handCards.Add(card);
            }
            
            handView.Sort(handCards);
        }
        public void Discard()
        {
            var selected = GetSelected();
            int completed = 0;

            foreach (var card in selected)
            {
                // Stop any previous tween to avoid conflicts
                card.transform.DOKill();

                card.transform.DOMove(GetManager<DeckManager>().transform.position, 0.3f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        card.Remove();
                        completed++;
                        if (completed >= selected.Count)
                        {
                            handCards.RemoveAll(x => selected.Contains(x));
                            Draw();
                            OnSelect?.Invoke();
                        }
                    });
            }
        }
        public void ClearHand()
        {
            var cards = handCards;
            int completed = 0;

            foreach (var card in cards)
            {
                // Stop any previous tween to avoid conflicts
                card.transform.DOKill();

                card.transform.DOMove(GetManager<DeckManager>().transform.position, 0.3f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        card.Remove();
                        completed++;
                        if (completed >= HandCount)
                        {
                            handCards.Clear();
                        }
                    });
            }
        }
    }

}
