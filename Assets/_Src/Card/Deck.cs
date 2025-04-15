using Balatro.Cards.CardsRuntime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Cards.System
{

    public class Deck
    {
        private List<CardData> cards = new List<CardData>();
        public void Initialize(List<CardData> startingCards)
        {
            cards =  new List<CardData>(startingCards);
            Shuffe();
        }
        public CardData Draw()
        {
            if (cards.Count == 0) return null;
            CardData card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
        public void Shuffe()
        {
            cards = cards.OrderBy(c => Random.value).ToList();
        }
    }
}

