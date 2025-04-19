using UnityEngine;

public class Deck : BaseCardCollection
{
    public override bool CanSelect()
    {
        return false;
    }
}
