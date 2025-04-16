
using System;
using System.Collections.Generic;
using System.Linq;


namespace Balatro.Cards.System
{

    public class Deck
    {
        private List<CardData> cards = new List<CardData>();
        public bool isEmpty => cards.Count == 0;
        public bool shuffeLock;
        public event Action OnDeckChanged;
        public void Initialize(List<CardData> startingCards)
        {
            cards =  new List<CardData>(startingCards);
            if (!shuffeLock)
            {
                Shuffe();
            }
            OnDeckChanged?.Invoke();

        }
        public List<CardData> GetCards()
        {
            return cards;
        }
        public CardData Draw()
        {
            if (cards.Count == 0) return null;
            CardData card = cards[0];
            cards.Remove(card);

            UnityEngine.Debug.Log("Card in deck: "+ card.name);
            OnDeckChanged?.Invoke();
            return card;
        }
        public void Shuffe()
        {
            cards = cards.OrderBy(c => UnityEngine.Random.value).ToList();
        }
    }
}

