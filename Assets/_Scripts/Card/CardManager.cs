using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] CardPile deck;
    [SerializeField] CardPile discardPile;
    [SerializeField] HandCard hand;
    [SerializeField] CardFactory cardFactory;
    [SerializeField] InputManager inputManager;

    private void Awake()
    {

        inputManager.OnDraw += DrawHand;
        inputManager.OnDiscard += DiscardHand;
        inputManager.OnPlay += PlayHand;
        inputManager.OnReset += ResetHand;
    }
    private void Start()
    {
        
    }

    private void ResetHand()
    {
       
    }

    private void PlayHand()
    {
        
    }

    private async void DiscardHand()
    {
        var selectedList = hand.TakeSelected();
        foreach (var card in selectedList)
        {
            discardPile.AddCard(card);
        }
        await discardPile.ResetPile();
        DrawHand();
    }

    private async void DrawHand()
    {
        int amount = hand.HandSize - hand.Count;
        if (amount > deck.Count)
        {
           await RefillDeck();
        }
        for (int i = 0; i < amount; i++) 
        {
            var card = deck.TakeCard(0);
            card.IsFront = true;
            hand.AddCard(card);
        }
        hand.ResetHand();
    }
    private async UniTask RefillDeck()
    {
        var list = discardPile.TakeAllCard();
        deck.AddCards(list);
        await deck.ResetPile(24);
        //await UniTask.Delay(1000);
    }
}
