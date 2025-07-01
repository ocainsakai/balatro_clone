using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "GameContext", menuName = "Scriptable Objects/GameContext")]
public class GameContext : ScriptableObject
{
    public ReactiveCollection<SerializableGuid> selectedCards = new();
    public List<SerializableGuid> deck = new();
    public List<SerializableGuid> hand = new();
    public List<SerializableGuid> discardPile = new();
    public List<SerializableGuid> playedCards = new ();
    public List<SerializableGuid> scoredCards = new ();

    public bool CanDarw => 8 - hand.Count > deck.Count;
    public void OnEnable()
    {
        ClearAll();
    }
    public void Discard2Deck()
    {
        deck.AddRange(discardPile);
        deck.Shuffle();
        discardPile.Clear();
    }
    
    public void Deck2Hand()
    {
        int amount = 8 - hand.Count;
        hand.RemoveAll(x => selectedCards.Contains(x));
        selectedCards.Clear();
        var cards = deck.Take(amount).ToList();
        deck.RemoveAll(x => cards.Contains(x));
        hand.AddRange(cards);
    }
    public void Select2Play()
    {
        playedCards.AddRange(selectedCards);
    }
    public void Play2Score(IEnumerable<CardData> cards)
    {
        //scoredCards.AddRange(playedCards.Where(x => cards.Contains(x.Data)));
    }
    public void ClearAll()
    {
        deck.Clear();
        selectedCards.Clear();
        discardPile.Clear();
        scoredCards.Clear();
        playedCards.Clear();
        hand.Clear();
    }
}
