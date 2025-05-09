using Game.Cards;
using Game.Player.Hands;
using Game.Pokers;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using VContainer;

namespace Game.System.Score
{
    public class ScoreManager : CardCollection
    {
        private BlindManager blindManager;
        private PokerViewModel viewModel;
        private HandViewModel handViewModel;
        private ReactiveProperty<int> roundScore = new();
        public IReadOnlyReactiveProperty<int> RoundScore => roundScore;
        public Subject<ConditionContext> OnScore = new Subject<ConditionContext>();
        public Subject<ConditionContext> PostScore = new Subject<ConditionContext>();
        public Subject<Unit> OnRoundEnd = new Subject<Unit>();
        [Inject]
        public ScoreManager(PokerViewModel pokerViewModel, PlayManager playManager, HandViewModel handViewModel, BlindManager blindManager)
        {
            this.blindManager = blindManager;
            this.viewModel = pokerViewModel;
            this.handViewModel = handViewModel;
            playManager.OnPlayed.Subscribe(x => OnPlayHandle());


        }
        private void OnPlayHandle()
        {
            var temp = new List<Card>();
            for (int i = 0; i < handViewModel.Count; i++)
            {
                var card = handViewModel.Cards[i];
                if (card.State.Value == CardState.Select)
                {
                    temp.Add(card);
                }
            }
            foreach(var card in temp)
            {
                card.State.Value = CardState.Play;
                Add(card);
                handViewModel.Remove(card);
            }
            Score();
        }

        public void Score()
        {
            foreach (var card in _cards)
            {
                ConditionContext onScoreContext = new ConditionContext(card.CardID);
                if (PokerEvaluator.comboCards.Contains(card.Data))
                {
                    card.Apply(viewModel);
                    OnScore.OnNext(onScoreContext);
                }
            }
            //PostScoreContext postScoreContext = new PostScoreContext(_cards.Select(x => x.Data).ToList());
            //PostScore.OnNext(postScoreContext);
            //ScoreCalculate();
        }
        public void ScoreCalculate()
        {
            roundScore.Value += (viewModel.Chip.Value * viewModel.Mult.Value);
            if (roundScore.Value >= blindManager.targetScore)
            {
                handViewModel.Clear();
                _cards.Clear();
                OnRoundEnd.OnNext(Unit.Default);
            }
            else
            {
                _cards.Clear();
                handViewModel.DrawHand();
            }
        }
       
    }
}