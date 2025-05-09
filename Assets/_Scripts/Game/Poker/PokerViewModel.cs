using Game.Cards;
using Game.Player.Hands;
using System.Linq;
using UniRx;
using VContainer;

namespace Game.Pokers
{
    public class PokerViewModel
    {
        //private HandViewModel handViewModel;
        public ReactiveProperty<PokerData> Data = new();
        public ReactiveProperty<int> Chip = new ReactiveProperty<int>();
        public ReactiveProperty<int> Mult = new();
        [Inject]
        public PokerViewModel(HandViewModel handViewModel)
        {
            //this.handViewModel = handViewModel;
            Data.Value = PokerDatabase.None;
            Data.Subscribe(x =>
            {
                Chip.Value = x.Chip;
                Mult.Value = x.Mult;
            });
            handViewModel.OnCardStateChanged.Subscribe(x =>
            {
                if (x.State.Value == CardState.Hold || x.State.Value == CardState.Select)
                {
                    
                    var result = PokerEvaluator.Evaluate(handViewModel.GetCardInState(CardState.Select).ToList());
                    Data.Value = result.poker;
                    PokerEvaluator.comboCards = result.comboCards;
                }
            });
        }

    }
}