using Game.Jokers;
using System.Collections;
using UniRx;

namespace Game.Cards
{
    public enum CardState
    {
        Hold,
        Selected,
        Played,
        Scored,
    }
    public class Card : EffectPlusChip

    {
        public CardData Data;
        
        public bool CanSelect;
       
        public ReactiveProperty<CardState> State = new ReactiveProperty<CardState>(CardState.Hold) ;
        public Card(CardData data)
        {
            Data = data;
        }
        
    }
}