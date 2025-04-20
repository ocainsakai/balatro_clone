using UnityEngine;
using UniRx;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] CardDatabase cardDatabase;
    [SerializeField] CardSpawner spawner;
    [SerializeField] PlayZone handZone;

    Hand hand;
    Deck deck;
    DiscardPile discardPile;
    //CardView.Spawner cardSpawner;
    int HandCount => hand.collection.Count;
    int HandSize = 8;
    private void Awake()
    {
        hand = new();
        deck = new();
        discardPile = new();
    }
    private void Start()
    {
        handZone.OnSort += hand.DeselectAll;
        deck?.SetCollection(cardDatabase.data);
        DrawCards();
    }
    public void DrawCards()
    {
        int amount = HandSize - HandCount;
        Debug.Log(amount);
        for (int i = 0; i < amount; i++)
        {
            var card = deck.DrawCard();
            hand.AddCard(card);
            spawner.SpawnCard(card);
        }
        handZone.Sort();
    }
    public void Discard()
    {
        var cards = hand.GetSelected();
        discardPile.AddCards(cards);
        hand.RemoveSelected();
        Debug.Log(hand.collection.Count);
    }
    public void SortByRank()
    {
        
    }
    public void SortBySuit()
    {

    }
}
