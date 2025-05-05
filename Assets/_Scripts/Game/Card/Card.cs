using Game.Jokers;
using System.Collections;
using UniRx;

namespace Game.Cards
{
    public enum CardState
    {
        OnHand,
        Selected,
        Played,
        Scored,
    }
    public class Card : IEffectPlusChip

    {
        public CardData Data;
        public int Value => Data.Rank == 14 ? 11 : Data.Rank > 10 ? 10 : Data.Rank;

        public int Chip => Value;

        public bool CanSelect;
       
        public ReactiveProperty<CardState> State = new ReactiveProperty<CardState>(CardState.OnHand) ;
        public Card(CardData data)
        {
            Data = data;
        }
    }
}