using Game.Cards;
using UnityEngine;
using VContainer;
using UniRx;
using System.Collections.Generic;


public class HandManager : CardCollctionView
{
    private int selectedCount => _cards.GetCardState(CardState.Select).Count;
    [Inject] DeckManager _deckManager;
    protected override void OnAddCard(Card card)
    {
        base.OnAddCard(card);
        card.State.Value = CardState.Hold;
        Card.CanSelect = selectedCount < 5;
        Card.Select.Subscribe(card => {
            Card.CanSelect = selectedCount < 5;
        });
    }
    public void Remove(Card card)
    {
        _cards.Remove(card);
    }
    public List<Card> GetSelectCards()
    {
        return _cards.GetCardState(CardState.Select);
    }
    public void RemoveSelectCards()
    {
        _cards.RemoveCardState(CardState.Select);
    }
    public void DrawHand()
    {
        _deckManager.Shuffle();
        int amount = 8 - _cards.Count;
        for (int i = 0; i < amount; i++)
        {
            var card = _deckManager.GetFirst();
            _cards.Add(card);
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            DrawHand();
        }
    }
}
