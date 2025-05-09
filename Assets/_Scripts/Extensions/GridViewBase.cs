using System.Threading.Tasks;
using Game.Cards;
using Game.System.Score;
using UniRx;
using UnityEngine;
using UnityEngine.XR;
using VContainer;

[RequireComponent (typeof(GridLayoutGO))]
public abstract class GridViewBase<T> : MonoBehaviour
{

    protected GridLayoutGO _layout => GetComponent<GridLayoutGO>();
    protected CardCollection _cards;
    protected CardFactory cardFactory;

    protected AsyncProcess mainProces = new AsyncProcess();
    //protected AsyncProcess discardProcess = new AsyncProcess();
    [Inject]
    public virtual void Construct(CardFactory cardFactory)
    {
        this.cardFactory = cardFactory;
    }
    protected virtual void Awake()
    {
        this.transform.ObserveChildCount().Subscribe(x =>
        {
            mainProces.Enqueue(() => _layout.RepositionChildren());
        });
        if (_cards != null)
        {
            _cards.Cards.ObserveAdd().Subscribe(x =>
            {
                mainProces.Enqueue(() => OnCardAdd(x.Value));
            });
            _cards.Sorted.Subscribe( x =>
            {
                mainProces.Enqueue(() => SynchronizeCards());
            });
            _cards.OnDiscardCard.Subscribe(x =>
            {
                mainProces.Enqueue(() => cardFactory.GetCardFormID(x.CardID).OnDiscard());
            });
        }
    }
    protected async Task SynchronizeCards()
    {
        for (int i = 0;  i < _cards.Count; i ++)
        {
            var cardView = cardFactory.GetCardFormID(_cards.Cards[i].CardID);
            cardView.transform.SetSiblingIndex(i);
        }
        await _layout.RepositionChildren();
    }
    protected async Task OnCardAdd(Card card)
    {
        var cardView = cardFactory.GetCardFormID(card.CardID);
        if (cardView == null)
        {
            cardView = cardFactory.CreateCard(card, transform);
        }
        else
        {
            cardView.transform.SetParent(this.transform);
        }
        await (_layout.RepositionChildren());
    }
}