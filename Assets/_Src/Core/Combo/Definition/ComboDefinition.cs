using Balatro.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Balatro.Combo.Logic
{
    [CreateAssetMenu(fileName = "ComboDefinition", menuName = "Scriptable Objects/ComboDefinition")]
    public class ComboDefinition : ScriptableObject
    {
        [SerializeField] ComboTypeData comboType;
        [SerializeField] List<ComboCondition> conditions;

        public bool AND;
        public ComboTypeData TryGetCombo(List<CardData> cards, out List<CardData> comboCard)
        {
            comboCard = new List<CardData>();
            List<CardData> remainingCards = new List<CardData>(cards);
            if (conditions != null)
            {
                foreach (var condition in conditions)
                {
                    if (!condition.IsSatisfied(remainingCards, out var matchedCards))
                    {
                        return null;
                    }

                    if (AND)
                    {
                        comboCard = new List<CardData>(matchedCards);
                    }
                    else
                    {
                        comboCard.AddRange(matchedCards);
                        remainingCards.RemoveAll(card => matchedCards.Contains(card));
                    }
                }
            }

            return comboType;
        }
    }
}
