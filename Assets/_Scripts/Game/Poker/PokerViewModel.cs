
using System.Collections.Generic;
using System.Linq;
using Game.Cards;
using UniRx;
using VContainer;

namespace Game.Pokers
{
    public class PokerViewModel
    {
        public ReactiveProperty<PokerData> Data = new();
        public ReactiveProperty<int> Chip = new ReactiveProperty<int>();
        public ReactiveProperty<int> Mult = new();
        public List<SerializableGuid> ComboCards = new List<SerializableGuid>();
        [Inject]
        public PokerViewModel(HandManager handManager)
        {
            Data.Value = PokerDatabase.None;
            Data.Subscribe(x =>
            {
                Chip.Value = x.Chip;
                Mult.Value = x.Mult;
            });
            Card.Select.Subscribe(x => {
                var result = PokerEvaluator.Evaluate(handManager.GetSelectCards());
                Data.Value = result.poker;
                ComboCards = result.comboCards?.Select(x => x.CardDataId).ToList();
            });
        }

    }
}