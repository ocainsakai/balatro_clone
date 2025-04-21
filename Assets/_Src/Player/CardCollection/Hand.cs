using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Hand : BaseCardCollection
{
    public List<CardModel> GetSelected() => collection.Where(x => x.IsSelected.Value).ToList();
    public List<CardModel> GetComboCards() => collection.Where(x => x.IsInCombo).ToList();
    public void DeselectAll()
    {
        collection.ForEach(x => x.ResetState());
    }
    public void RemoveSelected()
    {
        var selected = GetSelected();
        RemoveCards(selected);
    }
    public override bool CanSelect()
    {
        Debug.Log(GetSelected().Count);
        return GetSelected().Count < 5;
    }
}
