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
            var shuffled = _cards.OrderBy(x => Random.value).ToList();
            _cards.Clear();
            foreach (var card in shuffled)
            {
                _cards.Add(card);
            }
        }
    }
}