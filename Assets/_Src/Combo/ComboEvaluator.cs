using Balatro.Cards;
using Balatro.Cards.CardsRuntime;
using Balatro.Combo.Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Combo.Systems
{
    public class ComboEvaluator : MonoBehaviour
    {
        [SerializeField] CardsRSO SelectedCards;
        [SerializeField] List<ComboDefinition> definitions;
        [SerializeField] ComboTypeData highCard;
        [SerializeField] ComboTypeData none;
        [SerializeField] ComboVariable ComboVariable;

        private void Awake()
        {
            SelectedCards.OnListChanged += SelectedCards_OnUpdated;
        }

        private void SelectedCards_OnUpdated(List<Card> cards)
        {

            var cardsData = cards.Select(x => x.data).ToList();
            var comboCards = new List<CardData>();
            ComboVariable.ComboType = Evalutor(cardsData, out comboCards);
        }

        public ComboTypeData Evalutor(List<CardData> cards, out List<CardData> comboCard)
        {
            comboCard = new List<CardData>();
            if (cards.Count == 0)
            {
                return ComboVariable.None;
            }
            foreach (var defi in definitions)
            {
                var result = defi.TryGetCombo(cards, out comboCard);
                if (result != null)
                {
                    return result;
                }
            }
            comboCard.Add(cards.OrderBy(x => x.Rank).Last());
            return highCard;
        }
    }

}
