
namespace Poker
{
    [System.Serializable]
    public struct PokerHand : IPokerHand
    {
        public string Name;
        public int Chip;
        public int Mult;
        int IPokerHand.Chip => Chip;
        int IPokerHand.Mult => Mult;

        string IPokerHand.Name => Name;

        public PokerHand(string Name, int Chip, int Mult)
        {
            this.Name = Name;
            this.Chip = Chip;
            this.Mult = Mult;
        }
    }

}
