using Game.Cards;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using static Game.Cards.CardsSorter;

public class CardCollection : ItemCollection<Card>
{
    public Subject<Unit> Sorted = new Subject<Unit>();
    public void Sort(SortType sortType)
    {
        if (Count == 0) return;
        var sortedCards = (sortType == SortType.ByRank) ? CardsSorter.SortByRank(Items) : CardsSorter.SortBySuit(Items);
        for (int i = 0; i < sortedCards.Count(); i++)
        {
            var card = sortedCards.ElementAt(i);
            int currentIndex = _items.IndexOf(card);
            if (currentIndex != i)
            {
                _items.Move(currentIndex, i);
            }
        }
        Sorted.OnNext(Unit.Default);
    }
    public List<Card> GetCardState(CardState state)
    {
        return _items.Where(x => x.State.Value == state).ToList();
    }
    public void RemoveCardState(CardState state)
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            var card = _items[i];
            if (card.State.Value == state)
            {
                _items.RemoveAt(i);
            }
        }
    }
}