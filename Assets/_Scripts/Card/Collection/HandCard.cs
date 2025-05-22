using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;


public class HandCard : CardGrid
{
    public int SelectedCount => selectedCards.Count();
    public IEnumerable<Card> selectedCards => cards.Where(x => x.State.Value == Card.CardState.Select);
    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.State.AsObservable().Subscribe( _ =>
        {
            Card.CanSelect = SelectedCount < 5;
            PokerHandType poker = PokerChecker.GetBestHand(selectedCards).HandType;
            Debug.Log(poker);
        });
    }
    public void ResetHand()
    {
        foreach (Card card in cards)
        {
            card.ResetCard();
        }
        _ = layout.SequentialReposition(60);
    }
    public IEnumerable<Card> TakeSelected()
    {
        var list = new List<Card>(selectedCards);
        cards.RemoveAll(cards => list.Contains(cards));
        return list;
    }
}
