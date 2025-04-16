
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Cards.System
{

    public class Deck
    {
        private List<CardData> cards = new List<CardData>();
        public bool isEmpty => cards.Count == 0;
        public bool shuffeLock;
        public void Initialize(List<CardData> startingCards)
        {
            cards =  new List<CardData>(startingCards);
            if (!shuffeLock)
            {
                Shuffe();
            }
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

