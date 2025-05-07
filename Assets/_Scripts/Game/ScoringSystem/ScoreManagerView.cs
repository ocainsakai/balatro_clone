
using VContainer;
using UniRx;
using Game.Cards;

using System.Threading.Tasks;
using System.Linq;
namespace Game.System.Score
{
    public class ScoreManagerView : GridViewBase
    {
        [Inject] ScoreManager scoreManager;
        [Inject] CardFactory cardFactory;

        AsyncProcess drawProcess = new AsyncProcess();
        AsyncProcess discardProcess = new AsyncProcess();
        AsyncProcess scoreProcess = new AsyncProcess();
        private void Awake()
        {
            scoreManager.OnScore.Subscribe(x => scoreProcess.Enqueue(() => OnScoreCard(x.Card)));
            scoreManager.Cards.ObserveAdd().Subscribe(x =>
            {
                drawProcess.Enqueue(() => OnCardAdd(x.Value),
                    () => scoreManager.Score());
            });
            scoreManager.Cards.ObserveCountChanged().Subscribe( x =>
            {
                discardProcess.Enqueue(() => OnCountChanged(),
                    async () => await _layout.RepositionChildren());
            }).AddTo(this);
        }
        private async Task OnScoreCard(Card card)
        {
            var cardView = cardFactory.GetCardFormPool(card);
            cardView.ShowValue();
            await Task.Delay(200);
        }
        private async Task OnCardAdd(Card card)
        {
            var cardView = cardFactory.GetCardFormPool(card);
            cardView.transform.SetParent(transform);
            await (_layout.RepositionChildren());
        }
        private async Task OnCountChanged()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var cardView = transform.GetChild(i).GetComponent<CardView>();
                var card = cardView.GetCard();
                if (!scoreManager.Cards.Contains(card))
                {
                   await cardView.OnDiscard();
                }
            }
        }
    }
}