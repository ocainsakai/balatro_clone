using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Cards;
using Game.Pokers;
using UniRx;
using UnityEngine;
using VContainer;

public class ScoreManager : CardCollctionView
{
    [Inject] HandManager handManager;
    [Inject] PokerViewModel pokerViewModel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PLayCards();
        }
    }
    protected override async UniTask AddProcess(IList<CollectionAddEvent<Card>> buffer)
    {
        await base.AddProcess(buffer);
        await StartScore();
    }
    private void PLayCards()
    {
        var select = handManager.GetSelectCards();
        foreach (var card in select)
        {
            _cards.Add(card);
            handManager.Remove(card);
        }
    }

    protected override void OnAddCard(Card card)
    {
        card.State.Value = CardState.Play;
        base.OnAddCard(card);
    }
  


   
    public Subject<Card> OnScore = new Subject<Card>();
    private async UniTask StartScore()
    {
        foreach (var item in _cards.Items)
        {
            if (pokerViewModel.ComboCards.Contains(item.Data.CardDataId))
            {
                item.State.Value = CardState.Score;
               
                OnScore.OnNext(item);
            }
        }
        await UniTask.Yield();
    }

}
