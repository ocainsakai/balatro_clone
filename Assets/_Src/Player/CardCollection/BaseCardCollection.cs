using System.Collections.Generic;
using System.Linq;

public abstract class BaseCardCollection : ICardCollection
{
    
    public List<CardModel> collection { get; private set; }

    public bool CanShuffe;
    protected BaseCardCollection()
    {
        CanShuffe = true;
        collection = new List<CardModel>();
    }
    public virtual void SetCollection(List<CardData> collection)
    {
        foreach (var item in collection)
        {
            this.collection.Add(new CardModel(item, this));
        }
        Shuffe();
    }
    public virtual void SetCollection(List<CardModel> collection)
    {
        this.collection = collection;
        Shuffe();
    }
    public virtual void AddCard(CardModel card)
    {
        collection.Add(card);
        card.cardCollection = this;
    }

    public abstract bool CanSelect();

    public virtual void RemoveAllCards()
    {
        collection.Clear();
    }

    public void RemoveCard(CardModel card)
    {
        collection.Remove(card);
        card.ResetState();
    }

    public CardModel DrawCard(int id)
    {
        var card = collection[id];
        RemoveCard(card);
        return card;
    }

    public CardModel DrawCard()
    {
        return DrawCard(0);
    }

    public void Shuffe()
    {
        if (CanShuffe)
        collection = collection.OrderBy(x => UnityEngine.Random.value).ToList();
    }

    public void AddCards(List<CardModel> cards)
    {
        foreach (var item in cards)
        {
            AddCard(item);
        }
    }

    public void RemoveCards(List<CardModel> cards)
    {
        foreach (var item in cards)
        {
            RemoveCard(item);
        }
    }
}
public enum SortType
{
    Rank,
    Suit,
}