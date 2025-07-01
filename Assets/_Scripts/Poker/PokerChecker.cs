

using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class PokerChecker 
{
    public static PokerData GetPokerData(this PokerHandType result)
    {
        return Resources.Load<PokerData>("GameData/Poker/"+result);
    }
    public static PokerHandMatch GetBestHand(IEnumerable<Card> cards)
    {
        if (cards == null || cards.Count() == 0)
        {
            return PokerHandMatch.None;
        }
        var allPossible = GetAllPossibleHands(cards);
        if (allPossible.Any())
        {
            return allPossible.OrderByDescending(x => x.HandType).First();
        }
        var max = new List<CardData>() { cards.Max().Data};
        return new PokerHandMatch(PokerHandType.HighCard, max);
    }
    public static IEnumerable<PokerHandMatch> GetAllPossibleHands(IEnumerable<Card> cards)
    {
        int count = cards.Count();
        HashSet<PokerHandMatch> foundHands = new HashSet<PokerHandMatch>();
        var cardDatas = cards.Select(c => c.Data).ToList();

        if (count >= 5)
        {

            foundHands.Add(cardDatas.HasFlushFive());
            foundHands.Add(cardDatas.HasFlushHouse());
            foundHands.Add(cardDatas.HasFiveOfAKind());
            foundHands.Add(cardDatas.HasRoyalFlush());
            foundHands.Add(cardDatas.HasStraightFlush());
            foundHands.Add(cardDatas.HasFullHouse());
            foundHands.Add(cardDatas.HasFlush());
            foundHands.Add(cardDatas.HasStraight());
        }
        if (count >= 4)
        {
            foundHands.Add(cardDatas.HasFourOfAKind());
            foundHands.Add(cardDatas.HasTwoPair());

        }
        if (count >= 3)
        {

            foundHands.Add(cardDatas.HasThreeOfAKind());
        }
        if (count >= 2)
        {

            foundHands.Add(cardDatas.HasOnlyPair());
        }
        var max = new List<CardData>() { cards.OrderByDescending(x => x.Data.RankValue).First().Data };
        var high = new PokerHandMatch(PokerHandType.HighCard, max);
        foundHands.Add(high);
        return foundHands;
    }

}
