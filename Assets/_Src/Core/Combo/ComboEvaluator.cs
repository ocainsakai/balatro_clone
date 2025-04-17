using Balatro.Cards;
using Balatro.Cards.System;
using Balatro.Combo.Logic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Combo.Systems
{
    public class ComboEvaluator : BaseManager
    {
        [SerializeField] CardsRSO SelectedCards;
        [SerializeField] List<ComboDefinition> definitions;
        [SerializeField] ComboTypeData highCard;
        [SerializeField] ComboTypeData none;
        [SerializeField] ComboTypeRSO ComboVariable;

        public override void Initialize()
        {
            base.Initialize();
            var handManager = GetManager<HandManager>();
            handManager.OnSelect += SelectedCards_OnUpdated;
        }
        private void SelectedCards_OnUpdated()
        {
            var cards = GetManager<HandManager>().GetSelected();
            var cardData = cards.Select(c => c.data).ToList();
            ComboVariable.ComboType = Evalutor(cardData, out var matchedData);
            ComboVariable.comboCard = cards.Where(c => matchedData.Contains(c.data)).ToList();
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
