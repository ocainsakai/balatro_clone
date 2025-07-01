
using System.Collections.Generic;

public class PokerHandContext
{
    public IEnumerable<CardData> IsMatchCard;
    public PokerHandType HandType;
    public PokerHandContext(IEnumerable<CardData> isMatchCard, PokerHandType handType)
    {
        IsMatchCard = isMatchCard;
        HandType = handType;
    }
}