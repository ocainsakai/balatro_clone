
namespace Poker
{
   
    public static class PokerDatabase
    {
        public static PokerHand HighCard = new PokerHand("HighCard",5,1);
        public static PokerHand Pair = new PokerHand("Pair",10,2);
        public static PokerHand TwoPair = new PokerHand("TwoPair",20,2);
        public static PokerHand ThreeOfAKind = new PokerHand("ThreeOfAKind",30,3);
        public static PokerHand Straight = new PokerHand("Straight",30,4);
        public static PokerHand Flush = new PokerHand("Flush",35,4);
        public static PokerHand FullHouse = new PokerHand("FullHouse",40,4);
        public static PokerHand FourOfAkind = new PokerHand("FourOfAkind",60,7);
        public static PokerHand StraightFlush = new PokerHand("StraightFlush",100,8);
        public static PokerHand FiveOfAKind = new PokerHand("FiveOfAKind",120,12);
        public static PokerHand FlushHouse = new PokerHand("FlushHouse",140,14);
        public static PokerHand FlushFive = new PokerHand("FlushFive",160,16);
    }
}

