using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public abstract class BaseCardCollection : ICardCollection, IDisposable
{

    public List<CardModel> collection { get; private set; } = new List<CardModel>();

    private readonly ReactiveCommand<Unit> _onCardSelectionChanged = new();
    private readonly ReactiveCommand<CardModel> _onCardRemoved = new();
    private readonly List<IDisposable> cardSubscriptions = new();
    public IReactiveCommand<Unit> OnCardSelectionChanged => _onCardSelectionChanged;
    public IReactiveCommand<CardModel> OnCardRemoved=> _onCardRemoved;


    public bool CanShuffe;
    protected BaseCardCollection()
    {
        CanShuffe = true;
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
        var sub = card.IsSelected
            .Skip(1)
            .Subscribe(_ => OnCardSelectionChanged.Execute(Unit.Default));
        card.cardCollection = this;
    }

    public abstract bool CanSelect();

    public virtual void ClearCards()
    {
        RemoveCards(collection);
    }

    public void RemoveCard(CardModel card)
    {

        collection.Remove(card);
        card.ResetState();
        _onCardRemoved.Execute(card);
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
        {
            var shuffled = collection.OrderBy(x => UnityEngine.Random.value).ToList();
            collection = shuffled;
        }
    }

    public void AddCards(List<CardModel> cards)
    {
        foreach (var item in cards)
        {
            collection.Add(item);
        }
    }

    public void RemoveCards(List<CardModel> cards)
    {
        foreach (var card in cards)
        {
            RemoveCard(card);
        }
    }
    public void Dispose()
    {
        foreach (var subscription in cardSubscriptions)
        {
            subscription.Dispose();
        }
        cardSubscriptions.Clear();
        _onCardSelectionChanged.Dispose();
    }
}
public enum SortType
{
    Rank,
    Suit,
}