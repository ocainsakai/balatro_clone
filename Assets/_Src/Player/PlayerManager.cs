using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Hand hand;
    Deck deck;
    DiscardPile discardPile;
    private void Awake()
    {
        hand = new();
        deck = new();
        discardPile = new();
    }
}
