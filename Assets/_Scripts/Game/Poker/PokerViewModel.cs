using Game.Cards;
using Game.Jokers;
using Game.Player.Hands;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Pokers
{
    public class PokerViewModel
    {
        private HandViewModel handViewModel;
        public ReactiveProperty<PokerData> Data = new();
        public ReactiveProperty<int> Chip = new ReactiveProperty<int>();
        public ReactiveProperty<int> Mult = new();
        [Inject]
        public PokerViewModel(HandViewModel handViewModel)
        {
            this.handViewModel = handViewModel;
            Data.Value = PokerDatabase.None;
            Data.Subscribe(x =>
            {
                Chip.Value = x.Chip;
                Mult.Value = x.Mult;
            });
            handViewModel.OnCardStateChanged.Subscribe(x =>
            {
                if (x.State.Value == CardState.OnHand || x.State.Value == CardState.Selected)
                {
                    
                    var result = PokerEvaluator.Evaluate(handViewModel.GetCardInState(CardState.Selected).ToList());
                    Data.Value = result.poker;
                    PokerEvaluator.comboCards = result.comboCards;
                }
            });
        }
        public void ApplyEffect(object effect) {
            if (effect is IEffectPlusMult)
            {
                Debug.Log("+Mult");
                Mult.Value += (effect as IEffectPlusMult).Mult;
            }
            if (effect is IEffectPlusChip)
            {
                Debug.Log("+Chip");

                Chip.Value += (effect as IEffectPlusChip).Chip;
            }
        }

    }
}