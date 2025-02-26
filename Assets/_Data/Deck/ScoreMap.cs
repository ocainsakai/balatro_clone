using System.Collections.Generic;

public static class ScoreMap
{
    public static Dictionary<PokerHandType, int> BaseScores = new Dictionary<PokerHandType, int>
    {
        { PokerHandType.HighCard, 5 },
        { PokerHandType.Pair, 10 },
        { PokerHandType.TwoPair, 20 },
        { PokerHandType.ThreeOfAKind, 30 },
        { PokerHandType.Straight, 30 },
        { PokerHandType.Flush, 35 },
        { PokerHandType.FullHouse, 40 },
        { PokerHandType.FourOfAKind, 60 },
        { PokerHandType.StraightFlush, 100 },
        { PokerHandType.RoyalFlush, 100 },
        { PokerHandType.FiveOfAKind, 120 },
        { PokerHandType.FlushHouse, 140 },
        { PokerHandType.FlushFive, 160 }
    };
    public static Dictionary<PokerHandType, int> Multipliers = new Dictionary<PokerHandType, int>
    {
        { PokerHandType.HighCard, 1 },
        { PokerHandType.Pair, 2 },
        { PokerHandType.TwoPair, 2 },
        { PokerHandType.ThreeOfAKind, 3 },
        { PokerHandType.Straight, 4 },
        { PokerHandType.Flush, 4 },
        { PokerHandType.FullHouse, 4 },
        { PokerHandType.FourOfAKind, 7 },
        { PokerHandType.StraightFlush, 8 },
        { PokerHandType.RoyalFlush, 8 },
        { PokerHandType.FiveOfAKind, 12 },
        { PokerHandType.FlushHouse, 14 },
        { PokerHandType.FlushFive, 16 }

    };
}
