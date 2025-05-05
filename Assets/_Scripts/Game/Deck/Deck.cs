using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game.Cards.Decks
{
    public class Deck
    {
        public IEnumerable<CardData> _cards;
        public List<Card> cards = new List<Card>();
        public Deck() { 
            _cards = Resources.Load<CardDatabase>(nameof(CardDatabase)).cardList;
            foreach (var cardData in _cards)
            {
                var card = new Card(cardData);
                cards.Add(card);
            }
        }
        public void Shuffle()
        {
            cards = cards.OrderBy(x => UnityEngine.Random.value).ToList();
        }
    }
}