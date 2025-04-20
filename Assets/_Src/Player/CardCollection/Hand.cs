using System.Collections.Generic;
using System.Linq;


public class Hand : BaseCardCollection
{
    public List<CardModel> GetSelected() => collection.Where(x => x.IsSelected.Value).ToList();
    public void DeselectAll()
    {
        collection.ForEach(x => { x.IsSelected.Value = false; });
    }
    public void RemoveSelected()
    {
        var selected = GetSelected();
        RemoveCards(selected);
    }
    public override bool CanSelect()
    {
        
        return GetSelected().Count < 5;
    }
}
