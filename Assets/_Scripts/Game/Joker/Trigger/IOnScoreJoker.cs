using System.Collections;

namespace Game.Jokers
{
    public interface IOnScoreJoker
    {
        public bool Condition(OnScoreContext context);
    }
}