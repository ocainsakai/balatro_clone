using Game.Cards;
using System.Linq;
using UniRx;
using static Game.Cards.CardsSorter;

//public interface ICardCollection {
//    public IReadOnlyReactiveCollection<Card> Cards { get; }
//    public bool Contains(Card card);
//};
public abstract class CardCollection
{
    protected ReactiveCollection<Card> _cards = new ReactiveCollection<Card>();
    public IReadOnlyReactiveCollection<Card> Cards => _cards;
    
    public Subject<Unit> Sorted = new Subject<Unit>();
    public Subject<Card> OnDiscardCard = new Subject<Card>();

    public int Count => _cards.Count;
    public bool Contains(Card card)
    {
        return _cards.Contains(card);
    }
    public void Add(Card card)
    {
        _cards.Add(card);
    }
    public void Remove(Card card)
    {
        //OnDiscardCard.OnNext(card);
        _cards.Remove(card);
    }
    public Card GetFirst()
    {
        return TakeCard(0);
    }
    public Card TakeCard(int index)
    {
        var card = _cards[index];
        _cards.RemoveAt(index);
        return card;
    }
    public void Sort(SortType sortType)
    {
        if (Cards.Count == 0) return;
        var sortedCards = (sortType == SortType.ByRank) ? CardsSorter.SortByRank(Cards) : CardsSorter.SortBySuit(Cards);
        for (int i = 0; i < sortedCards.Count(); i++)
        {
            var card = sortedCards.ElementAt(i);
            int currentIndex = _cards.IndexOf(card);
            if (currentIndex != i)
            {
                _cards.Move(currentIndex, i);
            }
        }
        Sorted.OnNext(Unit.Default);
    }
}