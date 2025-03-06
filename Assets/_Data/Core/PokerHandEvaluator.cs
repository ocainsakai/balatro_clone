using System.Collections.Generic;
using System.Linq;

public class PokerHandEvaluator
{ 
   
    public static PokerHandType EvaluateHand(List<Card> hand)
    {
        var rankCounts = hand.GroupBy(c => c.cardSO.rank).ToDictionary(g => g.Key, g => g.Count());
        bool isFlush = hand.All(c => c.cardSO.suit == hand[0].cardSO.suit) && hand.Count == 5;
        var sortedRanks = hand.Select(c => c.cardSO.rank).OrderBy(r => r).ToList();
        bool isStraight = sortedRanks.Zip(sortedRanks.Skip(1), (a, b) => b - a).All(diff => diff == 1) && hand.Count == 5;
        
        
            if (isFlush && sortedRanks.SequenceEqual(new List<int> { 10, 11, 12, 13, 14 }))
                return PokerHandType.RoyalFlush;
            if (isFlush && isStraight)
                return PokerHandType.StraightFlush;
            if (rankCounts.ContainsValue(5)) return PokerHandType.FiveOfAKind;
            if (rankCounts.ContainsValue(3) && rankCounts.ContainsValue(2)) return PokerHandType.FullHouse;
            if (isFlush) return PokerHandType.Flush;
            if (isStraight) return PokerHandType.Straight;
        
            if (rankCounts.ContainsValue(4)) return PokerHandType.FourOfAKind;
            if (rankCounts.Count(v => v.Value == 2) == 2) return PokerHandType.TwoPair;
       
            if (rankCounts.ContainsValue(3)) return PokerHandType.ThreeOfAKind;
        
            if (rankCounts.ContainsValue(2)) return PokerHandType.Pair;
          
        return PokerHandType.HighCard;

    }
}