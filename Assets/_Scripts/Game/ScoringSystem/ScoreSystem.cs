using Game.Pokers;
using System.Threading.Tasks;
using UniRx;
using VContainer;

namespace Game.System.Score
{
    public class ScoreSystem : CardCollection
    {
        private BlindManager blindManager;
        private PokerViewModel viewModel;
        private ReactiveProperty<int> roundScore = new();
        public IReadOnlyReactiveProperty<int> RoundScore => roundScore;


        public Subject<Unit> OnRoundEnd = new Subject<Unit>();
        [Inject]
        public ScoreSystem(PokerViewModel pokerViewModel, RoundManager playManager, BlindManager blindManager)
        {
            this.blindManager = blindManager;
            this.viewModel = pokerViewModel;

        }
        public async Task Score()
        {
            foreach (var card in _items)
            {
               
                    await Task.Delay(2000);
                
            }
        }
       
    }
}