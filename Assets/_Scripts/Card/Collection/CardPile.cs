using Cysharp.Threading.Tasks;
using UnityEngine;
[RequireComponent(typeof(CardPileView))]
public class CardPile : CardCollectionAbstract
{
    protected CardPileView cardPileView;
    protected virtual void Awake()
    {
        cardPileView = GetComponent<CardPileView>();
    }
    public async UniTask ResetPile(int delayMS = 60)
    {
        await cardPileView.SequentialReposition(delayMS);
    }
    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.ChangeState(Card.CardState.None);

    }
}