using Cysharp.Threading.Tasks;

public class Arrowhead : JokerCard
{
    ChipEffect chipEffect => GetComponent<ChipEffect>();
    public override UniTask OnScoredActive(Card card)
    {
        if (card.Data.Suit == CardSuit.Spades) 
            return chipEffect.Add(card.transform, 50);
        else return base.OnScoredActive(card);
    }
}
