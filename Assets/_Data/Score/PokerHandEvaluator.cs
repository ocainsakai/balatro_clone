using System.Collections.Generic;
using System.Linq;

public class PokerHandEvaluator
{
    public static int CalculateHandScore(List<CardSO> hand)
    {
        Dictionary<int, int> valueCounts = new Dictionary<int, int>();
        Dictionary<Suit, int> suitCounts = new Dictionary<Suit, int>();
        List<int> values = new List<int>();

        foreach (CardSO card in hand)
        {
            if (!valueCounts.ContainsKey(card.value))
                valueCounts[card.value] = 0;
            valueCounts[card.value]++;

            if (!suitCounts.ContainsKey(card.suit))
                suitCounts[card.suit] = 0;
            suitCounts[card.suit]++;
            values.Add(card.value);
        }
        values.Sort();
        bool isStraight = values.Distinct().Count() == 5 && (values.Max() - values.Min() == 4);
        // four of kind
        if (valueCounts.ContainsValue(4)) return 50;
        // full horse
        if (valueCounts.ContainsValue(3) && valueCounts.ContainsValue(2)) return 40;
        // Flush
        if (suitCounts.ContainsValue(5)) return 35;
        // straight
        if (isStraight) return 45;
        // Three of a Kind
        if (valueCounts.ContainsValue(3)) return 30;
        // two pair
        if (valueCounts.Count(v => v.Value == 2) == 2) return 20; // Two Pair
        // One Pair
        if (valueCounts.ContainsValue(2)) return 10; 

        return 5;
    }
}