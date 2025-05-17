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
                _items.Add(card);
            }
            Debug.Log(_items.Count);
        }
        public void Shuffle()
        {
            var shuffled = _items.OrderBy(x => Random.value).ToList();
            _items.Clear();
            foreach (var card in shuffled)
            {
                _items.Add(card);
            }
        }
    }
}