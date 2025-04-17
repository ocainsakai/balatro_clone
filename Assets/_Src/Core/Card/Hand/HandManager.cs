using Balatro.Cards.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Cards.System
{
    public class HandManager : BaseManager
    {
        [SerializeField] CardFactory cardFactory;
        [SerializeField] HandView handView;


        public List<Card> handCards;
        public int HandSzie = 8;
        public int HandCount => handCards.Count;
        public int SelectCount => GetSelected().Count;

        public override void Initialize()
        {
            base.Initialize();
            handView.handCards = handCards;
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
            }
            else if (SelectCount < 5)
            {
                card.Select();
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
            handView.Sort();
        }
        public void Discard()
        {
            var selected = GetSelected();
            foreach (var card in selected)
            {
                handCards.Remove(card);
                card.transform.DOMove(GetManager<DeckManager>().transform.position,0.3f)
                            .SetEase(Ease.OutQuad)
                            .OnComplete( () =>
                            {
                                card.Remove();
                            }) ;
            }
            //handCards.RemoveAll(x => x.CardView.isSelected);
            //selected.ForEach(x => { x.CardView.Remove(); });

            Draw();
        }
    }

}
