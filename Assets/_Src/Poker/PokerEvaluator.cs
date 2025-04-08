
using Balatro.Card;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Balatro.Poker
{
    [System.Serializable]
    public struct PokerHand
    {
        public string Name;
        public int Chip;
        public int Mult;
        public PokerHand(string Name, int Chip, int Mult)
        {
            this.Name = Name;
            this.Chip = Chip;
            this.Mult = Mult;
        }
        public static PokerHand None = new PokerHand("Poker Hand", 0, 0);
    }

    public class PokerResult
    {
        public PokerHand PokerHand;
        public List<ICard> ValidCards;
        public PokerResult(PokerHand poker, List<ICard> validCards)
        {
            PokerHand = poker;
            ValidCards = validCards ?? new List<ICard>();
        }
    }
    public static class PokerEvaluator
    {
        public static int FlushSize = 5;
        public static int StraightSize = 5;
        public static int StraightGap = 1;
        public static PokerResult Evaluator(List<ICard> cards)
        {

            if (cards == null || cards.Count == 0) return null;
            var evaluationRules = new Func<List<ICard>, PokerResult>[]
            {
                EvaluatorFlushFive,
                EvaluatorFlushHouse,
                EvaluateFiveOfAKind,
                EvaluateRoyalStraight,
                EvaluateStraightFlush,
                EvaluateFullHouse,
                EvaluateFourOfAKind,
                EvaluateFlush,
                EvaluateStraight,
                EvaluateThreeOfAKind,
                EvaluateTwoPair,
                EvaluatePair,
            };
            foreach (var rule in evaluationRules)
            {
                var result = rule(cards);
                if (result != null)
                    return result;
            }
            return EvaluateHighCard(cards);
        }

        private static PokerResult EvaluateHighCard(List<ICard> list)
        {
            var highCard = new List<ICard> { list.OrderBy(card => card.Rank).Last() };
            return new PokerResult(PokerDatabase.HighCard, highCard);
        }

        private static PokerResult EvaluatePair(List<ICard> list)
        {
            var pairGroup = list.GroupBy(card => card.Rank)
                                .FirstOrDefault(g => g.Count() == 2)?.ToList();
            if (pairGroup != null)
            {
                return new PokerResult(PokerDatabase.Pair, pairGroup);
            }
            return null;
        }

        private static PokerResult EvaluateTwoPair(List<ICard> list)
        {
            var pairGroups = list.GroupBy(card => card.Rank)
                             .Where(g => g.Count() == 2)
                             .ToList();

            if (pairGroups.Count == 2)
            {
                return new PokerResult(PokerDatabase.TwoPair, pairGroups.SelectMany(g => g).ToList());
            }
            return null;
        }

        private static PokerResult EvaluateThreeOfAKind(List<ICard> list)
        {
            if (IsKindOf(list, 3))
            {
                return new PokerResult(PokerDatabase.ThreeOfAKind, list.GroupBy(card => card.Rank)
                                   .FirstOrDefault(g => g.Count() == 3).ToList());
            }
            return null;
        }

        private static PokerResult EvaluateStraight(List<ICard> list)
        {
            if (IsStraight(list))
            {
                return new PokerResult(PokerDatabase.Straight, list);
            }
            return null;
        }

        private static PokerResult EvaluateFlush(List<ICard> list)
        {
            if (IsFlush(list))
            {
                return new PokerResult(PokerDatabase.Flush, list);
            }
            return null;
        }

        private static PokerResult EvaluateFourOfAKind(List<ICard> list)
        {
            if (IsKindOf(list, 4))
            {
                return new PokerResult(PokerDatabase.FourOfAkind, list.GroupBy(card => card.Rank)
                                   .FirstOrDefault(g => g.Count() == 4).ToList());
            }
            return null;
        }

        private static PokerResult EvaluateFullHouse(List<ICard> list)
        {
            if (IsFullHouse(list))
            {
                return new PokerResult(PokerDatabase.FullHouse, list);
            }
            return null;
        }

        private static PokerResult EvaluateStraightFlush(List<ICard> list)
        {
            if (IsFlush(list) && IsStraight(list))
            {

                return new PokerResult(PokerDatabase.StraightFlush, list);
            }
            return null;
        }

        private static PokerResult EvaluateRoyalStraight(List<ICard> list)
        {
            if (IsFlush(list) && IsStraight(list))
            {

                return new PokerResult(PokerDatabase.StraightFlush, list);
            }
            return null;
        }

        private static PokerResult EvaluateFiveOfAKind(List<ICard> list)
        {
            if (IsKindOf(list, 5))
            {
                return new PokerResult(PokerDatabase.FiveOfAKind, list);
            }
            return null;
        }

        static PokerResult EvaluatorFlushFive(List<ICard> cards)
        {
            if (IsFlush(cards) && IsKindOf(cards, 5))
            {

                return new PokerResult(PokerDatabase.FlushFive, cards);
            }
            return null;
        }
        static PokerResult EvaluatorFlushHouse(List<ICard> cards)
        {
            if (IsFlush(cards) && IsFullHouse(cards))
            {
                return new PokerResult(PokerDatabase.FlushHouse, cards);
            }
            return null;
        }
        static bool IsFullHouse(List<ICard> cards)
        {
            var groups = cards.GroupBy(card => card.Rank)
                         .Select(g => new { Rank = g.Key, Count = g.Count(), Cards = g.ToList() })
                         .OrderByDescending(g => g.Count)
                         .ToList();
            return (groups.Count >= 2 && groups[0].Count == 3 && groups[1].Count == 2);
        }
        static bool IsKindOf(List<ICard> cards, int number)
        {
            return cards.GroupBy(card => card.Rank)
                                   .FirstOrDefault(g => g.Count() == number) != null;

        }
        static bool IsFlush(List<ICard> cards)
        {
            if (cards.Count < FlushSize) return false;
            return cards.GroupBy(card => card.Suit).Count() == 1;
        }
        static bool IsStraight(List<ICard> cards)
        {
            if (cards.Count < StraightSize) return false;

            var originalValues = cards.Select(card => card.Rank).OrderBy(v => v).ToList();
            bool isOriginalStraight = IsStraightSequence(originalValues);

            // Nếu không có rank 14 (Ace), chỉ cần kiểm tra danh sách gốc
            if (!originalValues.Contains(14)) return isOriginalStraight;

            // Tạo danh sách thay thế: thay rank 14 thành rank 1
            var alternateValues = originalValues.Select(v => v == 14 ? 1 : v).OrderBy(v => v).ToList();

            // Kiểm tra sảnh với cả hai danh sách
            return isOriginalStraight || IsStraightSequence(alternateValues);
        }
        static bool IsStraightSequence(List<int> values)
        {
            return values.Zip(values.Skip(1), (a, b) => b - a)
                         .All(diff => diff == 1 && diff <= StraightGap);
        }
    }


        public static class PokerDatabase
        {
            public static PokerHand HighCard = new PokerHand("HighCard", 5, 1);
            public static PokerHand Pair = new PokerHand("Pair", 10, 2);
            public static PokerHand TwoPair = new PokerHand("TwoPair", 20, 2);
            public static PokerHand ThreeOfAKind = new PokerHand("ThreeOfAKind", 30, 3);
            public static PokerHand Straight = new PokerHand("Straight", 30, 4);
            public static PokerHand Flush = new PokerHand("Flush", 35, 4);
            public static PokerHand FullHouse = new PokerHand("FullHouse", 40, 4);
            public static PokerHand FourOfAkind = new PokerHand("FourOfAkind", 60, 7);
            public static PokerHand StraightFlush = new PokerHand("StraightFlush", 100, 8);
            public static PokerHand FiveOfAKind = new PokerHand("FiveOfAKind", 120, 12);
            public static PokerHand FlushHouse = new PokerHand("FlushHouse", 140, 14);
            public static PokerHand FlushFive = new PokerHand("FlushFive", 160, 16);
        }
    

}
