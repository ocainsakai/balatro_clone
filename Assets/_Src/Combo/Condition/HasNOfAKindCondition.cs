using Balatro.Cards;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Combo.Logic
{
    [CreateAssetMenu(fileName = "HasNOfAKindCondition", menuName = "Scriptable Objects/HasNOfAKindCondition")]
    public class HasNOfAKindCondition : ComboCondition
    {

        public int count;
        public override bool IsSatisfied(List<CardData> cards, out List<CardData> matchedCards)
        {
            matchedCards = new List<CardData>();
            var matchingGroup = cards
            .GroupBy(card => card.Rank)
            .FirstOrDefault(group => group.Count() == count);
            if (matchingGroup != null)
            {
                matchedCards = matchingGroup.ToList();
                return true;
            }

            return false;
        }
    }

}
