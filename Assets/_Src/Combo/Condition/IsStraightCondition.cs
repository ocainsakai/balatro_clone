using Balatro.Cards;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Combo.Logic
{


    [CreateAssetMenu(fileName = "IsStraightCondition", menuName = "Scriptable Objects/IsStraightCondition")]
    public class IsStraightCondition : ComboCondition
    {
        public int count;
        public int gap;

        public override bool IsSatisfied(List<CardData> cards, out List<CardData> matchedCards)
        {
            matchedCards = new List<CardData>();

            var orderedRanks = cards
            .Select(c => c.Rank)
            .Distinct()
            .OrderBy(r => r)
            .ToList();

            if (orderedRanks.Count < count)
                return false;

            var match = GetSoftStraightSequence(orderedRanks);
            if (match != null)
            {
                matchedCards = cards.Where(c => match.Contains(c.Rank)).ToList();
                return true;
            }

            // Xử lý trường hợp A (14) như 1
            if (!orderedRanks.Contains(14))
                return false;

            var convertedRanks = orderedRanks
                .Select(r => r == 14 ? 1 : r)
                .Distinct()
                .OrderBy(r => r)
                .ToList();

            var altMatch = GetSoftStraightSequence(convertedRanks);
            if (altMatch != null)
            {
                matchedCards = cards.Where(c => altMatch.Contains(c.Rank == 1 ? 14 : c.Rank)).ToList();
                return true;
            }

            return false;
        }
        private List<int> GetSoftStraightSequence(List<int> values)
        {
            var distinct = values.Distinct().OrderBy(v => v).ToList();

            for (int i = 0; i < distinct.Count; i++)
            {
                List<int> sequence = new List<int> { distinct[i] };
                int prev = distinct[i];

                for (int j = i + 1; j < distinct.Count; j++)
                {
                    int diff = distinct[j] - prev;

                    if (diff > 0 && diff <= gap)
                    {
                        sequence.Add(distinct[j]);
                        prev = distinct[j];

                        if (sequence.Count >= count)
                            return sequence;
                    }
                    else if (diff > gap)
                    {
                        break;
                    }
                }
            }

            return null;
        }
    }  
}