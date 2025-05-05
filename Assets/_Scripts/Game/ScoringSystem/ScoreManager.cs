using Game.Cards;
using Game.Pokers;
using System.Linq;
using UniRx;
using VContainer;

namespace Game.System.Score
{
    public class ScoreManager
    {
        [Inject] PokerViewModel viewModel;

        private ReactiveCollection<Card> playedCards = new ReactiveCollection<Card>();
        public IReadOnlyReactiveCollection<Card> PlayedCards => playedCards;

        private ReactiveProperty<int> roundScore = new();
        public IReadOnlyReactiveProperty<int> RoundScore => roundScore;
        public Subject<OnScoreContext> OnScore = new Subject<OnScoreContext>();
        public Subject<PostScoreContext> PostScore = new Subject<PostScoreContext>();
       


        public void Add(Card card)
        {
            playedCards.Add(card);
            card.State.Value = CardState.Played;
        }
        public void Score()
        {
            foreach (var card in playedCards)
            {
                OnScoreContext onScoreContext = new OnScoreContext(card);
                if (PokerEvaluator.comboCards.Contains(card.Data))
                {
                    viewModel.ApplyEffect(card);
                    OnScore.OnNext(onScoreContext);
                }
            }
            PostScoreContext postScoreContext = new PostScoreContext(playedCards.Select(x => x.Data).ToList());
            PostScore.OnNext(postScoreContext);
        }
        public void ScoreCalculate()
        {
            roundScore.Value += (viewModel.Chip.Value * viewModel.Mult.Value);
        }
    }
}