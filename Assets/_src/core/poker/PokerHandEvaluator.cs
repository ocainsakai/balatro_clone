using Balatro.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

public class PokerHandResult
{
    public PokerHandType HandType { get; set; }
    public List<CardDataSO> ValidCards { get; set; }
}

public class PokerHandType
{
    public string Name { get; }
    public int BaseChips { get; }
    public int BaseMultiple { get; }

    private PokerHandType(string name, int baseChips, int baseMultiple)
    {
        Name = name;
        BaseChips = baseChips;
        BaseMultiple = baseMultiple;
    }

    public static readonly PokerHandType None = new PokerHandType("None", 0, 0);
    public static readonly PokerHandType HighCard = new PokerHandType("High Card", 5, 1);
    public static readonly PokerHandType Pair = new PokerHandType("Pair", 10, 2);
    public static readonly PokerHandType TwoPair = new PokerHandType("Two Pair", 20, 2);
    public static readonly PokerHandType ThreeOfAKind = new PokerHandType("Three of a Kind", 30, 3);
    public static readonly PokerHandType Straight = new PokerHandType("Straight", 30, 4);
    public static readonly PokerHandType Flush = new PokerHandType("Flush", 35, 4);
    public static readonly PokerHandType FullHouse = new PokerHandType("Full House", 40, 4);
    public static readonly PokerHandType FourOfAKind = new PokerHandType("Four of a Kind", 60, 7);
    public static readonly PokerHandType StraightFlush = new PokerHandType("Straight Flush", 100, 8);
}

public static class PokerHandEvaluator
{
    private const int FlushSize = 5;
    private const int StraightSize = 5;
    private const int StraightGap = 1;

    public static PokerHandResult EvaluateHand(List<CardDataSO> hand)
    {
        if (hand.Count == 0) return new PokerHandResult
        {
            HandType = PokerHandType.None,
            ValidCards = new List<CardDataSO>()
        }; ;
        // Thứ tự ưu tiên các tay bài từ cao xuống thấp
        var evaluationRules = new Func<List<CardDataSO>, PokerHandResult>[]
        {
            //EvaluateRoyalStraightFlush,
            EvaluateStraightFlush,
            EvaluateFullHouse,
            EvaluateFourOfAKind,
            //EvaluateRoyalStraight,
            EvaluateFlush,
            EvaluateStraight,
            EvaluateThreeOfAKind,
            EvaluateTwoPair,
            EvaluatePair,
            EvaluateHighCard
        };

        foreach (var rule in evaluationRules)
        {
            var result = rule(hand);
            if (result.HandType != PokerHandType.None)
                return result;
        }
        return new PokerHandResult
        {
            HandType = PokerHandType.None,
            ValidCards = new List<CardDataSO>()
        }; ;
    }

    //private static PokerHandResult EvaluateRoyalStraightFlush(List<CardDataSO> hand)
    //{
    //    if (IsFlush(hand) && IsRoyalStraight(hand))
    //    {
    //        return new PokerHandResult
    //        {
    //            HandType = PokerHandType.StraightFlush,
    //            ValidCards = hand
    //        };
    //    }
    //    return new PokerHandResult { HandType = PokerHandType.None };
    //}

    private static PokerHandResult EvaluateStraightFlush(List<CardDataSO> hand)
    {
        if (IsFlush(hand) && IsStraight(hand))
        {
            return new PokerHandResult
            {
                HandType = PokerHandType.StraightFlush,
                ValidCards = hand
            };
        }
        return new PokerHandResult { HandType = PokerHandType.None };
    }

    private static PokerHandResult EvaluateFullHouse(List<CardDataSO> hand)
    {
        var groups = hand.GroupBy(card => card.rank)
                         .Select(g => new { Rank = g.Key, Count = g.Count(), Cards = g.ToList() })
                         .OrderByDescending(g => g.Count)
                         .ToList();

        if (groups.Count >= 2 && groups[0].Count == 3 && groups[1].Count == 2)
        {
            return new PokerHandResult
            {
                HandType = PokerHandType.FullHouse,
                ValidCards = groups[0].Cards.Concat(groups[1].Cards).ToList()
            };
        }
        return new PokerHandResult { HandType = PokerHandType.None };
    }

    private static PokerHandResult EvaluateFourOfAKind(List<CardDataSO> hand)
    {
        var fourOfAKindGroup = hand.GroupBy(card => card.rank)
                                   .FirstOrDefault(g => g.Count() == 4);

        if (fourOfAKindGroup != null)
        {
            return new PokerHandResult
            {
                HandType = PokerHandType.FourOfAKind,
                ValidCards = fourOfAKindGroup.ToList()
            };
        }
        return new PokerHandResult { HandType = PokerHandType.None };
    }

    //private static PokerHandResult EvaluateRoyalStraight(List<CardDataSO> hand)
    //{
    //    if (IsStraight(hand) && hand.Any(card => card.IsRoyal()))
    //    {
    //        return new PokerHandResult
    //        {
    //            HandType = PokerHandType.Straight,
    //            ValidCards = hand
    //        };
    //    }
    //    return new PokerHandResult { HandType = PokerHandType.None };
    //}

    private static PokerHandResult EvaluateFlush(List<CardDataSO> hand)
    {
        var flushCards = hand.GroupBy(card => card.suit)
                             .FirstOrDefault(g => g.Count() >= FlushSize);

        if (flushCards != null)
        {
            return new PokerHandResult
            {
                HandType = PokerHandType.Flush,
                ValidCards = flushCards.ToList()
            };
        }
        return new PokerHandResult { HandType = PokerHandType.None };
    }

    private static PokerHandResult EvaluateStraight(List<CardDataSO> hand)
    {
        if (IsStraight(hand))
        {
            return new PokerHandResult
            {
                HandType = PokerHandType.Straight,
                ValidCards = hand
            };
        }
        return new PokerHandResult { HandType = PokerHandType.None };
    }

    private static PokerHandResult EvaluateThreeOfAKind(List<CardDataSO> hand)
    {
        var threeOfAKindGroup = hand.GroupBy(card => card.rank)
                                    .FirstOrDefault(g => g.Count() == 3);

        if (threeOfAKindGroup != null)
        {
            return new PokerHandResult
            {
                HandType = PokerHandType.ThreeOfAKind,
                ValidCards = threeOfAKindGroup.ToList()
            };
        }
        return new PokerHandResult { HandType = PokerHandType.None };
    }

    private static PokerHandResult EvaluateTwoPair(List<CardDataSO> hand)
    {
        var pairGroups = hand.GroupBy(card => card.rank)
                             .Where(g => g.Count() == 2)
                             .ToList();

        if (pairGroups.Count == 2)
        {
            return new PokerHandResult
            {
                HandType = PokerHandType.TwoPair,
                ValidCards = pairGroups.SelectMany(g => g).ToList()
            };
        }
        return new PokerHandResult { HandType = PokerHandType.None };
    }

    private static PokerHandResult EvaluatePair(List<CardDataSO> hand)
    {
        var pairGroup = hand.GroupBy(card => card.rank)
                            .FirstOrDefault(g => g.Count() == 2);

        if (pairGroup != null)
        {
            return new PokerHandResult
            {
                HandType = PokerHandType.Pair,
                ValidCards = pairGroup.ToList()
            };
        }
        return new PokerHandResult { HandType = PokerHandType.None };
    }

    private static PokerHandResult EvaluateHighCard(List<CardDataSO> hand)
    {
        
        var highCard = hand.OrderBy(card => card.rank).Last();
        return new PokerHandResult
        {
            HandType = PokerHandType.HighCard,
            ValidCards = new List<CardDataSO> { highCard }
        };
    }

    private static bool IsFlush(List<CardDataSO> hand)
    {
        return hand.GroupBy(card => card.suit).Any(g => g.Count() >= FlushSize);
    }

    private static bool IsStraight(List<CardDataSO> hand)
    {
        if (hand.Count < StraightSize) return false;
        var values = hand.Select(card => card.rank).OrderBy(v => v).ToList();

        for (int i = 0; i < values.Count - 1; i++)
        {
            int diff = values[i + 1] - values[i];
            if (diff > StraightGap || diff == 0) return false;
        }

        return true;
    }

    //private static bool IsRoyalStraight(List<CardDataSO> hand)
    //{
    //    return IsStraight(hand) && hand.Any(card => card.IsRoyal());
    //}

    // Phương thức tính điểm riêng biệt
    //public static int CalculateScore(PokerHandResult handResult)
    //{
    //    int additionalChips = handResult.ValidCards.Sum(card => card.baseValue);
    //    return handResult.HandType.BaseChips + (handResult.HandType.BaseMultiple * additionalChips);
    //}
}