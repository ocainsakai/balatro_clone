using Game.Cards;
using System.Linq;
using UniRx;
using static Game.Cards.CardsSorter;

public abstract class CardCollection
{
    protected ReactiveCollection<Card> _cards = new ReactiveCollection<Card>();
    public IReadOnlyReactiveCollection<Card> Cards => _cards;
    public int Count => _cards.Count;
    public void Add(Card card)
    {
        _cards.Add(card);
    }
    public void Remove(Card card)
    {
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
    }
}