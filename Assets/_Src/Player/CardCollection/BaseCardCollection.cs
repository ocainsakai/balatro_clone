using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardCollection : ICardCollection
{
    public List<CardModel> collection { get; private set; }
    public virtual void SetCollection(List<CardModel> collection)
    {
        this.collection = collection;
    }
    public virtual void AddCard(CardModel card)
    {
        collection.Add(card);
    }

    public abstract bool CanSelect();

    public virtual void RemoveAllCards()
    {
        collection.Clear();
    }

    public void RemoveCard(CardModel card)
    {
        collection.Remove(card);
    }
}
