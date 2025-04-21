using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayZone : MonoBehaviour, ICardSorter
{
    [SerializeField] GridLayoutGO layout;
    public event Action OnSort;
    enum SortType
    {
        None,
        Rank,
        Suit,
    }
    SortType type;
    public void Sort()
    {
        if (type == SortType.Rank)
        {
            SortByRank();
        }else if (type == SortType.Suit)
        {
            SortBySuit();
        } else
        layout.RepositionChildren();

        OnSort?.Invoke();
    }

    public void SortByRank()
    {
        type = SortType.Rank;
        var sorted = GetAllChildren(transform)
            .OrderBy(child => CardData.ParseRank(child.name))
            .ThenBy(child => CardData.ParseSuit(child.name))
            .ToList();

        for (int i = 0; i < sorted.Count; i++)
        {
            sorted[i].SetSiblingIndex(i);
        }
        layout.RepositionChildren();
        OnSort?.Invoke();

    }

    public void SortBySuit()
    {
        type = SortType.Suit;
        var sorted = GetAllChildren(transform)
            .OrderBy(child => CardData.ParseSuit(child.name))
            .ThenBy(child => CardData.ParseRank(child.name))
            .ToList();

        for (int i = 0; i < sorted.Count; i++)
        {
            sorted[i].SetSiblingIndex(i);
        }
        layout.RepositionChildren();
        OnSort?.Invoke();

    }
    public IEnumerable<Transform> GetAllChildren(Transform container)
    {
        for (int i = 0; i < container.childCount; i++)
            yield return container.GetChild(i);
    }

}