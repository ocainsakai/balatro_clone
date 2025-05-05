using System.Linq;

namespace Game.Jokers
{
    [JokerLogic("JollyJoker")]
    public class LogicJollyJoker : IJokerLogic, IPostScoreJoker, IEffectPlusMult
    {
        public int Mult => 8;

        public bool Condition(PostScoreContext context)
        {
            var rankGroup = context.cards.GroupBy(x => x.Rank).OrderByDescending(x => x.Count()).ToArray();
            return rankGroup[0].Count() >= 2;
        }
    }
}