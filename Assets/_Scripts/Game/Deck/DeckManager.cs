using Game.Cards;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private CardCollection _cards = new CardCollection();
    private void Awake()
    {
        var cardList = Resources.Load<CardDatabase>(nameof(CardDatabase)).cardList;
        foreach (var item in cardList)
        {
            var card = new Card(item);
            CardView.CreateCard(card, transform);
            _cards.Add(card);
        }
    }
    public void Shuffle()
    {

    }
    public Card GetFirst()
    {
        return _cards.GetFirst();
    }
}
