//using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
//using DG.Tweening;
using Game.Cards;
using UniRx;
using UnityEngine;

public abstract class CardCollctionView : GridView
{
    protected CardCollection _cards = new CardCollection();
    protected virtual void Awake()
    {
       this._cards.Items.ObserveAdd()
            .BatchFrame(1, FrameCountType.EndOfFrame)
            .Subscribe(
           async buffer => 
            {
                await AddProcess(buffer);

            });
        this._cards.Items.ObserveRemove()
            .BatchFrame(1, FrameCountType.Update)
            .Subscribe(async x =>
            {
                await RemoveProcess(x);
            });
    }
    protected async UniTask RemoveProcess(IList<CollectionRemoveEvent<Card>> list)
    {
        foreach (var card in list)
        {
            var cardView = CardView.GetCardView(card.Value.CardID);
            cardView.transform.SetParent(null);
            await _layout.RepositionChildren();
        }
    }
    protected virtual async UniTask AddProcess(IList<CollectionAddEvent<Card>> buffer)
    {
        foreach (var card in buffer)
        {
            var tasks = new List<UniTask>();
            OnAddCard(card.Value);
            CardView cardView = AddCardView(card.Value);
            await _layout.RepositionChildren();
        }
    }

    protected virtual void OnAddCard(Card card)
    {
        card.IsFlip.Value = true;
    }
    protected virtual CardView AddCardView(Card card)
    {
        var cardView = CardView.GetCardView(card.CardID);
        cardView.transform.SetParent(transform);
        return cardView;
    }
}
