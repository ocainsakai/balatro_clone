using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokerHandEvaluator
{
    public PokerHand Evaluate(List<CardModel> cards)
    {
        cards.ForEach(card => card.IsInCombo = false);

        var validCards = cards.Where(c => c.cardData != null).ToList();
        if (validCards.Count == 0) return PokerHand.None;
        
        var groupedByRank = validCards.GroupBy(card => card.cardData.Rank)
                                      .OrderByDescending(g => g.Count())
                                      .ToList();
        var groupedBySuit = validCards.GroupBy(card => card.cardData.Suit).ToList();

        bool isFlush = groupedBySuit.Any(g => g.Count() >= 5);
        bool isStraight = TryGetStraight(validCards, out var straightCards);

        if (isFlush && isStraight)
        {
            foreach (var c in straightCards) c.IsInCombo = true;
            return PokerHand.StraightFlush;
        }

        var four = groupedByRank.FirstOrDefault(g => g.Count() == 4);
        if (four != null)
        {
            foreach (var c in four) c.IsInCombo = true;
            return PokerHand.FourOfAKind;
        }

        var three = groupedByRank.FirstOrDefault(g => g.Count() == 3);
        var pair = groupedByRank.Skip(1).FirstOrDefault(g => g.Count() >= 2);
        if (three != null && pair != null)
        {
            foreach (var c in three.Concat(pair)) c.IsInCombo = true;
            return PokerHand.FullHouse;
        }

        var flush = groupedBySuit.FirstOrDefault(g => g.Count() >= 5);
        if (flush != null)
        {
            foreach (var c in flush.Take(5)) c.IsInCombo = true;
            return PokerHand.Flush;
        }

        if (isStraight)
        {
            foreach (var c in straightCards) c.IsInCombo = true;
            return PokerHand.Straight;
        }

        if (three != null)
        {
            foreach (var c in three) c.IsInCombo = true;
            return PokerHand.ThreeOfAKind;
        }

        var pairs = groupedByRank.Where(g => g.Count() == 2).Take(2).ToList();
        if (pairs.Count == 2)
        {
            foreach (var p in pairs)
                foreach (var c in p)
                    c.IsInCombo = true;
            return PokerHand.TwoPair;
        }

        if (groupedByRank[0]?.Count() == 2)
        {
            Debug.Log(cards.Count);
            foreach (var c in groupedByRank[0]) c.IsInCombo = true;
            return PokerHand.Pair;
        }

        var high = validCards.OrderByDescending(c => c.cardData.Rank).FirstOrDefault();
        if (high != null) high.IsInCombo = true;
        return PokerHand.HighCard;
    }

    private bool TryGetStraight(List<CardModel> hand, out List<CardModel> straightCards)
    {
        var uniqueRanks = hand.Select(c => c.cardData.Rank).Distinct().OrderBy(x => x).ToList();
        if (uniqueRanks.Count < 5)
        {
            straightCards = null;
            return false;
        }

        if (uniqueRanks.Contains(CardRank.Ace))
            uniqueRanks.Insert(0, CardRank.LowAce);

        for (int i = 0; i <= uniqueRanks.Count - 5; i++)
        {
            var seq = uniqueRanks.Skip(i).Take(5).ToList();
            if (seq.Zip(seq.Skip(1), (a, b) => b - a).All(diff => diff == 1))
            {
                straightCards = new List<CardModel>();
                var availableCards = new List<CardModel>(hand); 

                foreach (var rank in seq)
                {
                    var match = availableCards.FirstOrDefault(c => c.cardData.Rank == rank);
                    if (match != null)
                    {
                        straightCards.Add(match);
                        availableCards.Remove(match); 
                    }
                }

                return true;
            }
        }

        straightCards = null;
        return false;
    }
}
