using UnityEngine;

public class DiscardPile : BaseCardCollection
{
    public override bool CanSelect()
    {
        return false;
    }
}
