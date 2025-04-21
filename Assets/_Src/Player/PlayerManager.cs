using UnityEngine;
using UniRx;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] CardDatabase cardDatabase;
    [SerializeField] CardSpawner spawner;
    [SerializeField] PlayZone handZone;
    [SerializeField] PokerHandUI PokerUI;
    Hand hand;
    Deck deck;
    DiscardPile discardPile;
    PokerHandEvaluator handEvaluator;
    int HandCount => hand.collection.Count;
    int HandSize = 8;
    private void Awake()
    {
        hand = new();
        deck = new();
        discardPile = new();
        handEvaluator = new PokerHandEvaluator();
        handZone.OnSort += hand.DeselectAll;
        deck?.SetCollection(cardDatabase.data);
        hand.OnCardSelectionChanged.Subscribe(x =>
        {
            var poker = handEvaluator.Evaluate(hand.GetSelected());
            PokerUI.Show(poker);
            Debug.Log(hand.GetComboCards().Count);
        }).AddTo(this);
    }

    public void DrawCards()
    {
        int amount = HandSize - HandCount;
        if (amount > deck.collection.Count) {
            deck.AddCards(discardPile.collection);
            discardPile.ClearCards();
        }
        for (int i = 0; i < amount; i++)
        {
            var card = deck.DrawCard();
            hand.AddCard(card);
            var view = spawner.SpawnCard(card, hand);
        }
        handZone.Sort();
    }
    public void Discard()
    {
        var cards = hand.GetSelected();
        discardPile.AddCards(cards);
        hand.RemoveSelected();
        DrawCards();
    }
    public void SortByRank()
    {
        
    }
    public void SortBySuit()
    {

    }
}
