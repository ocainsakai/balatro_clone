using Game.Cards;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Player.Hands
{

    public  class HandView : GridViewBase
    {
        [Inject] CardFactory _cardFactory;
        [Inject] HandViewModel hand;
        [Inject] PlayManager gameManager;

        private AsyncProcess drawProcess = new AsyncProcess();
        //private AsyncProcess moveProcess = new AsyncProcess();
        private AsyncProcess removeProcess = new AsyncProcess();
        private void Awake()
        {
            hand.Cards.ObserveAdd().Subscribe(x =>
                drawProcess.Enqueue(() => Draw(x),() => hand.Sort(CardsSorter.SortType.ByRank))).AddTo(this);
            hand.Cards.ObserveMove().Subscribe(async x => await OnCardMoved(x)).AddTo(this);
            hand.Cards.ObserveRemove().Subscribe(x => removeProcess.Enqueue(() => _layout.RepositionChildren()));
            gameManager.OnDiscard.Subscribe(async x => await Discard()).AddTo(this);
        }
        

        private async Task OnCardMoved(CollectionMoveEvent<Card> moveEvent)
        {
            Debug.Log(moveEvent);
            Transform card = _layout.transform.GetChild(moveEvent.OldIndex);
            card.SetSiblingIndex(moveEvent.NewIndex);
            await _layout.RepositionChildren();
        }
 
        private async Task Draw(CollectionAddEvent<Card> cardEvent)
        {
            var cardData = cardEvent.Value;
            _cardFactory.CreateCard(cardData, transform);
            await _layout.RepositionChildren();
        }
        private async Task Discard()
        {
            foreach (var item in hand.Cards)
            {
                if (item.State.Value != CardState.Hold)
                {
                    var cardView = _cardFactory.GetCardFormPool(item);
                    await cardView.OnDiscard();
                }
            }
        }

    }
}