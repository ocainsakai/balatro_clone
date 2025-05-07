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
        public Subject<OnScoreContext> OnScore = new Subject<OnScoreContext>();
        public Subject<PostScoreContext> PostScore = new Subject<PostScoreContext>();
        //public Subject<int> OnScoreCalculated = new Subject<int>();
        [Inject]
        public ScoreManager(PokerViewModel pokerViewModel, PlayManager playManager, HandViewModel handViewModel, BlindManager blindManager)
        {
            this.blindManager = blindManager;
            this.viewModel = pokerViewModel;
            this.handViewModel = handViewModel;
            playManager.OnPlayed.Subscribe( x => OnPlayHandle());
        }
        private void OnPlayHandle()
        {
            var temp = new List<Card>();
            for (int i = 0; i < handViewModel.Count; i++)
            {
                var card = handViewModel.Cards[i];
                if (card.State.Value == CardState.Selected)
                {
                    temp.Add(card);
                }
            }
            foreach(var card in temp)
            {
                card.State.Value = CardState.Played;
                Add(card);
                handViewModel.Remove(card);
            }
        }

        public void Score()
        {
            foreach (var card in _cards)
            {
                OnScoreContext onScoreContext = new OnScoreContext(card);
                if (PokerEvaluator.comboCards.Contains(card.Data))
                {
                    viewModel.ApplyEffect(card);
                    OnScore.OnNext(onScoreContext);
                }
            }
            PostScoreContext postScoreContext = new PostScoreContext(_cards.Select(x => x.Data).ToList());
            PostScore.OnNext(postScoreContext);
        }
        public void ScoreCalculate()
        {
            roundScore.Value += (viewModel.Chip.Value * viewModel.Mult.Value);
            //OnScoreCalculated.OnNext(roundScore.Value);
            if (roundScore.Value >= blindManager.targetScore)
            {

            } else
            {
                _cards.Clear();
                handViewModel.DrawHand();
            }
        }
       
    }
}