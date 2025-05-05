using UnityEngine;

namespace Game.Jokers
{
    [JokerLogic("Joker")]
    public class LogicJoker : IJokerLogic, IEffectPlusMult, IPostScoreJoker
    {
        public int Mult => 4;

        public bool Condition(PostScoreContext context)
        {
            Debug.Log("joker");
            return true;
        }
    }
}