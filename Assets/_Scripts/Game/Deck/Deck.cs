using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game.Cards.Decks
{
    public class Deck : CardCollection
    {
        public IEnumerable<CardData> _cardData;
        public Deck() {
            _cardData = Resources.Load<CardDatabase>(nameof(CardDatabase)).cardList;
            foreach (var cardData in _cardData)
            {
                var card = new Card(cardData);
                _cards.Add(card);
            }
        }
        public void Shuffle()
        {
            var cards = new List<Card>(Cards);
            cards = cards.OrderBy(x => Random.value).ToList();
            for (int i = 0; i <  Count; i++)
            {
                int newIndex = cards.IndexOf(_cards[i]);
                _cards.Move(i, newIndex);
            }
        }
    }
}