using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour
{
    public static Dictionary<Card, Transform> Cards = new Dictionary<Card, Transform>();
    public static Dictionary<CardData, Card> CardDatas = new Dictionary<CardData, Card>();

    [SerializeField] Transform CardPrf;

   
    public Card CreateCard(CardData card, Sprite cardBack, Transform parent = null)
    {
        Transform newCardObj = Instantiate(CardPrf, parent);
        newCardObj.name = card.CardName;
        var newCard = newCardObj.GetComponent<Card>();
        newCard.SetData(card, cardBack);

        CardDatas[card] = newCard;
        Cards[newCard] = newCardObj;
        return newCard;
    }
    public Transform GetCardGO(Card card)
    {
        return Cards.TryGetValue(card, out  var cardObj) ? cardObj : null;
    }
    public Card GetCard(CardData cardData)
    {
        return CardDatas.TryGetValue(cardData, out var card) ? card : null;
    }
}
