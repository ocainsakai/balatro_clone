using Game.Jokers;
using System.Collections.Generic;
using UniRx;

namespace Game.Cards
{
    public enum CardState
    {
        Hold,
        Select,
        Play,
        Score,
    }
    public class Card : EffectPlusChip
    {
        public static Dictionary<SerializableGuid, Card> _cards = new Dictionary<SerializableGuid, Card>();
        public SerializableGuid CardID;
        public CardData Data;
        
        public bool CanSelect;
       
        public ReactiveProperty<CardState> State = new ReactiveProperty<CardState>(CardState.Hold) ;
        public Card(CardData data)
        {
            CardID = SerializableGuid.NewGuid();
            _cards[CardID] = this;
            Data = data;
        }
        public void Dispose()
        {
            // Unregister from static dictionary
            if (_cards.ContainsKey(CardID))
            {
                _cards.Remove(CardID);
            }

            // Clean up Rx or other managed data if needed
            State.Dispose();
        }
    }
}