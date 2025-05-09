using Game.Cards;

namespace Game
{
    public class OnScoreContext
    {
        public Card Card;
        public OnScoreContext(Card card)
        {
            Card = card;
        }
    }
}