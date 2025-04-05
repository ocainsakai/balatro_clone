using Card;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Poker
{
    public class PokerResult
    {
        public PokerHand PokerHand;
        public List<IStandardCard> ValidCards;
        public PokerResult(PokerHand poker, List<IStandardCard> validCards)
        {
            PokerHand = poker;
            ValidCards = validCards ?? new List<IStandardCard>(); 
        }
    }
    public static class PokerEvaluator
    {
        public static int FlushSize = 5;
        public static int StraightSize = 5;
        public static int StraightGap = 1;
        public static PokerResult Evaluator(List<IStandardCard> cards)
        {

            if(cards == null || cards.Count == 0 ) return null;
            var evaluationRules = new Func<List<IStandardCard>, PokerResult>[]
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

        private static PokerResult EvaluateHighCard(List<IStandardCard> list)
        {
            var highCard = new List<IStandardCard> { list.OrderBy(card => card.Rank).Last() };
            return new PokerResult(PokerDatabase.HighCard, highCard );
        }

        private static PokerResult EvaluatePair(List<IStandardCard> list)
        {
            var pairGroup = list.GroupBy(card => card.Rank)
                                .FirstOrDefault(g => g.Count() == 2)?.ToList();
            if (pairGroup != null)
            {
                return new PokerResult(PokerDatabase.Pair, pairGroup);
            }
            return null;
        }

        private static PokerResult EvaluateTwoPair(List<IStandardCard> list)
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

        private static PokerResult EvaluateThreeOfAKind(List<IStandardCard> list)
        {
            if (IsKindOf(list, 3))
            {
                return new PokerResult(PokerDatabase.ThreeOfAKind, list.GroupBy(card => card.Rank)
                                   .FirstOrDefault(g => g.Count() == 3).ToList());
            }
            return null;
        }

        private static PokerResult EvaluateStraight(List<IStandardCard> list)
        {
            if (IsStraight(list))
            {
                return new PokerResult(PokerDatabase.Straight, list);
            }
            return null;
        }

        private static PokerResult EvaluateFlush(List<IStandardCard> list)
        {
            if (IsFlush(list))
            {
                return new PokerResult(PokerDatabase.Flush, list);
            }
            return null;
        }

        private static PokerResult EvaluateFourOfAKind(List<IStandardCard> list)
        {
            if (IsKindOf(list, 4))
            {
                return new PokerResult(PokerDatabase.FourOfAkind, list.GroupBy(card => card.Rank)
                                   .FirstOrDefault(g => g.Count() == 4).ToList());
            }
            return null;
        }

        private static PokerResult EvaluateFullHouse(List<IStandardCard> list)
        {
            if (IsFullHouse(list))
            {
                return new PokerResult(PokerDatabase.FullHouse, list);
            }
            return null;
        }

        private static PokerResult EvaluateStraightFlush(List<IStandardCard> list)
        {
            if (IsFlush(list) && IsStraight(list))
            {

                return new PokerResult(PokerDatabase.StraightFlush, list);
            }
            return null;
        }

        private static PokerResult EvaluateRoyalStraight(List<IStandardCard> list)
        {
            if (IsFlush(list) && IsStraight(list))
            {

                return new PokerResult(PokerDatabase.StraightFlush, list);
            }
            return null;
        }

        private static PokerResult EvaluateFiveOfAKind(List<IStandardCard> list)
        {
            if (IsKindOf(list, 5))
            {
                return new PokerResult(PokerDatabase.FiveOfAKind, list);
            }
            return null;
        }

        static PokerResult EvaluatorFlushFive(List<IStandardCard> cards)
        {
            if (IsFlush(cards) && IsKindOf(cards,5))
            {
                
                return new PokerResult(PokerDatabase.FlushFive, cards);
            }
            return null;
        }
        static PokerResult EvaluatorFlushHouse(List<IStandardCard> cards)
        {
            if (IsFlush(cards) && IsFullHouse(cards))
            {
                return new PokerResult(PokerDatabase.FlushHouse, cards);
            }
            return null;
        }
        static bool IsFullHouse(List<IStandardCard> cards)
        {
            var groups = cards.GroupBy(card => card.Rank)
                         .Select(g => new { Rank = g.Key, Count = g.Count(), Cards = g.ToList() })
                         .OrderByDescending(g => g.Count)
                         .ToList();
            return (groups.Count >= 2 && groups[0].Count == 3 && groups[1].Count == 2) ;
        }
        static bool IsKindOf(List<IStandardCard> cards, int number)
        {
            return cards.GroupBy(card => card.Rank)
                                   .FirstOrDefault(g => g.Count() == number) != null;

        }
        static bool IsFlush(List<IStandardCard> cards)
        {
            if (cards.Count < FlushSize) return false;
            return cards.GroupBy(card => card.Suit).Count() == 1;
        }
        static bool IsStraight(List<IStandardCard> cards)
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
}

