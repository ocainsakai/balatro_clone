namespace Game.Jokers
{
    public interface IPostScoreJoker
    {
        public bool Condition(PostScoreContext context);
    }
}