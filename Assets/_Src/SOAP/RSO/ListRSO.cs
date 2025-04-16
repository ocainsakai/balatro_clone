
using System;
using System.Collections.Generic;


public class ListRSO<T> : RSO
{
    public List<T> List;
    public event Action<List<T>> OnListChanged;
    public event Action<T> OnAdded;
    public event Action<T> OnRemoved;
    public void AddCard(T item)
    {
        List.Add(item);
        OnAdded?.Invoke(item);
        OnListChanged?.Invoke(List);
    }

    public void RemoveCard(T item)
    {
        List.Remove(item);
        OnRemoved?.Invoke(item);
        OnListChanged?.Invoke(List);

    }

    public void Clear()
    {
        List.Clear();
        OnListChanged?.Invoke(List);
    }
    protected override void OnReset()
    {
        List = new List<T>();
        OnListChanged?.Invoke(List);
    }

    
}
