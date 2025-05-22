using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CardCollectionAbstract : MonoBehaviour
{
    protected List<Card> cards = new List<Card>();
    public int Count => cards.Count;
    public virtual void AddCard(Card card)
    {
        cards.Add(card);
        card.transform.parent = transform;
    }
    public virtual void AddCards(IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            AddCard(card);
        }
    }
    public virtual IEnumerable<Card> TakeAllCard()
    {
        var list = new List<Card>(cards);
        cards.Clear();
        return list;
    }
    public virtual Card TakeCard(int index)
    {
        if (cards.Count <= index) return null;
        var card = cards[index];
        cards.RemoveAt(index);
        return card;
    }
}
