using Game.Cards;
using System.Collections.Generic;
using System.Linq;

namespace Game.Pokers
{
    public static class PokerEvaluator
    {
        public struct PokerResult
        {
            public PokerData poker;
            public List<CardData> comboCards;
        }
        public static List<SerializableGuid> comboCardsID = new List<SerializableGuid>();
        public static PokerResult Evaluate(List<Card> cards)
        {
            var result = new PokerResult() { poker = PokerDatabase.None};
            if (cards.Count == 0 ) return result;
            var ranks = cards.Select(c => (int) c.Data.Rank).ToList();
            var suits = cards.Select(c => c.Data.Suit).ToList();

            var rankGroups = ranks.GroupBy(r => r).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).ToList();
            var suitGroups = suits.GroupBy(s => s).OrderByDescending(g => g.Count()).ToList();

            bool isFlush = suitGroups[0].Count() >= 5;
            bool isStraight = IsStraight(ranks, out List<int> straightRanks);

            if (isFlush && isStraight)
            {
                var flushSuit = suitGroups[0].Key;
                result.comboCards = cards
                    .Where(c => c.Data.Suit == flushSuit && straightRanks.Contains((int)c.Data.Rank))
                    .Select(c => c.Data)
                    .ToList();

                result.poker =  PokerDatabase.StraightFlush;
                return result;
            }
            if (rankGroups[0].Count() == 4)
            {
                result.poker = PokerDatabase.FourOfAKind;
                result.comboCards = cards.Where(c =>(int)c.Data.Rank == rankGroups[0].Key)
                    .Select(c => c.Data)
                    .ToList();
                return result;
            }

            // Full House
            if (rankGroups[0].Count() == 3 && rankGroups.Count > 1 && rankGroups[1].Count() >= 2)
            {
                result.poker = PokerDatabase.FullHouse;
                result.comboCards = cards.Where(c =>
                   (int)  c.Data.Rank == rankGroups[0].Key || (int) c.Data.Rank == rankGroups[1].Key)
                    .Select(c => c.Data)
                    .ToList();
                return result;
            }

            // Flush
            if (isFlush)
            {
                var flushSuit = suitGroups[0].Key;
                result.poker = PokerDatabase.Flush;
                result.comboCards = cards
                    .Where(c => c.Data.Suit == flushSuit)
                    .OrderByDescending(c => c.Data.Rank)
                    .Take(5)
                    .Select(c => c.Data)

                    .ToList();
                return result;
            }

            // Straight
            if (isStraight)
            {
                result.poker = PokerDatabase.Straight;
                result.comboCards = cards
                    .Where(c => straightRanks.Contains((int)c.Data.Rank))
                    .Select(c => c.Data)
                    .ToList();
                return result;
            }

            // Three of a Kind
            if (rankGroups[0].Count() == 3)
            {
                result.poker = PokerDatabase.ThreeOfAKind;
                result.comboCards = cards.Where(c => (int)c.Data.Rank == rankGroups[0].Key).Select(c => c.Data).ToList();
                return result;
            }

            // Two Pair
            if (rankGroups[0].Count() == 2 && rankGroups.Count > 1 && rankGroups[1].Count() == 2)
            {
                result.poker = PokerDatabase.TwoPair;
                var pairRanks = new[] { rankGroups[0].Key, rankGroups[1].Key };
                result.comboCards = cards.Where(c => pairRanks.Contains((int)c.Data.Rank)).Select(c => c.Data).ToList();
                return result;
            }

            // One Pair
            if (rankGroups[0].Count() == 2)
            {
                result.poker = PokerDatabase.Pair;
                result.comboCards = cards.Where(c => (int)c.Data.Rank == rankGroups[0].Key).Select(c => c.Data).ToList();
                return result;
            }

            // High Card
            result.poker = PokerDatabase.HighCard;
            result.comboCards = cards.OrderByDescending(c => c.Data.Rank).Take(1)
                    .Select(c => c.Data)
                    .ToList();
            return result;
        }
        private static bool IsStraight(List<int> ranks, out List<int> straightRanks)
        {
            straightRanks = new List<int>();
            var distinct = ranks.Distinct().OrderBy(x => x).ToList();

            for (int i = 0; i <= distinct.Count - 5; i++)
            {
                if (distinct[i + 4] - distinct[i] == 4)
                {
                    straightRanks = distinct.Skip(i).Take(5).ToList();
                    return true;
                }
            }

            // Special case: A-2-3-4-5 (Ace as 1)
            if (distinct.Contains(14) && distinct.Contains(2) && distinct.Contains(3) &&
                distinct.Contains(4) && distinct.Contains(5))
            {
                straightRanks = new List<int> { 2, 3, 4, 5, 14 };
                return true;
            }

            return false;
        }
    }
}