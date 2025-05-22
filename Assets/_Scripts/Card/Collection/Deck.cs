
using System.Collections.Generic;
using UnityEngine;

public class Deck : CardPile
{
    [SerializeField] CardFactory cardFactory;
    [SerializeField] Sprite cardBack;
    [SerializeField] StandardCards standardCards;
    private List<CardData> cardDatas => standardCards.cards;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        for (var i = 0; i < 25; i++)
        {
            var cardData = cardDatas[i];
            var card = cardFactory.CreateCard(cardData, cardBack, transform);
            cards.Add(card);
        }
    }
    public override void AddCard(Card card)
    {
        base.AddCard(card);
        card.IsFront = false;
    }
}
