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
    public class Card
    {
        public SerializableGuid CardID;
        public CardData Data;
        public ReactiveProperty<CardState> State = new ReactiveProperty<CardState>(CardState.Hold);
        public ReactiveProperty<bool> IsFlip = new ReactiveProperty<bool>(false);
        
        public static bool CanSelect;
        public static Subject<Card> Select = new Subject<Card>();

        private static Dictionary<SerializableGuid, Card> _cardDict = new Dictionary<SerializableGuid, Card>();
        public static Card GetCard(SerializableGuid cardId)
        {
            return _cardDict.TryGetValue(cardId, out Card result) ? result : null;
        }
        public Card(CardData data)
        {
            CardID = SerializableGuid.NewGuid();
            _cardDict[CardID] = this;
            Data = data;
        }
    }
}