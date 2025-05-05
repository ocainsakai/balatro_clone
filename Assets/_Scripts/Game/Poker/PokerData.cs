namespace Game.Pokers
{
    public struct PokerData
    {
        public string Name;
        public int Chip;
        public int Mult;
    }
    public static class PokerDatabase
    {
        public static PokerData None = new PokerData()
        {
            Name = "None",
            Chip = 0,
            Mult = 0,
        };
        public static PokerData HighCard = new PokerData()
        {
            Name = "High Card",
            Chip = 5,
            Mult = 1,
        };
        public static PokerData Pair = new PokerData()
        {
            Name = "Pair",
            Chip = 10,
            Mult = 2,
        }; public static PokerData TwoPair = new PokerData()
        {
            Name = "Two Pair",
            Chip = 20,
            Mult = 2,
        }; public static PokerData ThreeOfAKind = new PokerData()
        {
            Name = "Three Of A kind",
            Chip = 30,
            Mult = 3,
        }; public static PokerData Straight = new PokerData()
        {
            Name = "Straight",
            Chip = 30,
            Mult = 4,
        }; public static PokerData Flush = new PokerData()
        {
            Name = "Flush",
            Chip = 35,
            Mult = 4,
        }; public static PokerData FullHouse = new PokerData()
        {
            Name = "Full House",
            Chip = 40,
            Mult = 4,
        }; public static PokerData FourOfAKind = new PokerData()
        {
            Name = "Four Of A Kind",
            Chip = 60,
            Mult = 7,
        }; public static PokerData StraightFlush = new PokerData()
        {
            Name = "Straight Flush",
            Chip = 100,
            Mult = 8,
        };
    }
}