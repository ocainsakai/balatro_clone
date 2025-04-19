using UnityEngine;

public class DiscardPile : BaseCardCollection
{
    public override bool CanSelect()
    {
        //throw new System.NotImplementedException();
        return false;
    }
}
