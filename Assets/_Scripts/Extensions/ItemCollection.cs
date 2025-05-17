using UniRx;

public class ItemCollection<T>
{
    protected ReactiveCollection<T> _items;
    public IReadOnlyReactiveCollection<T> Items => _items;

    public Subject<T> OnDiscardItem;
    public ItemCollection()
    {
        _items = new ReactiveCollection<T>();
        OnDiscardItem = new Subject<T>();
    }

    public int Count => _items.Count;
    public bool Contains(T item)
    {
        return _items.Contains(item);
    }
    public void Add(T item)
    {
        _items.Add(item);
    }
    public void Remove(T item)
    {
        OnDiscardItem.OnNext(item);
        _items.Remove(item);
    }
    public T GetFirst()
    {
        return TakeCard(0);
    }
    public T TakeCard(int index)
    {
        var item = _items[index];
        _items.RemoveAt(index);
        return item;
    }
}