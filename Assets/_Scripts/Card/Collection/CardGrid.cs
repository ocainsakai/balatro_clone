using UnityEngine;

[RequireComponent (typeof(CardGridView))]
public class CardGrid : CardCollectionAbstract
{
    protected CardGridView layout;
    public int HandSize = 8;
    protected virtual void Awake()
    {
        layout = GetComponent<CardGridView>();
    }
}
