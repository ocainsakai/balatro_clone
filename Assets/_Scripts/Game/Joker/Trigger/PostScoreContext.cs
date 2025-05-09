using Game.Cards;
using System.Collections.Generic;

public class PostScoreContext
{
    public List<CardData> cards;
    public PostScoreContext(List<CardData> cards)
    {
        this.cards = cards;
    }
}