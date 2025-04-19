using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : BaseCardCollection
{
    public List<CardModel> GetSelected() => collection.Where(x => x.isSelected).ToList();
    public override bool CanSelect()
    {
        return GetSelected().Count < 5;
    }
}
