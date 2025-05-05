namespace Game.Jokers
{
    public class JokerCard
    {
        public JokerCardData Data;
        public IJokerLogic jokerLogic;
        public JokerCard(JokerCardData data, IJokerLogic logic) {
            this.Data = data;
            this.jokerLogic = logic;
        }
    }
}