using Game.Cards;
using System.Collections;
using UnityEngine;

namespace Game.Jokers
{
    [JokerLogic("GreedyJoker")]
    public class LogicGreedyJoker : IOnScoreJoker, IJokerLogic, IEffectPlusMult
    {
        public int Mult => 3;
        public bool Condition(OnScoreContext context)
        {
            return context.Card.Data.Suit == CardSuit.Diamonds;
        }
    }
}