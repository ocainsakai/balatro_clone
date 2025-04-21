using UnityEngine;

[CreateAssetMenu(menuName = "Poker/PokerHandData")]
public class PokerHandData : ScriptableObject
{
    public PokerHand handType;
    public string displayName;
    public string description;
    public int baseScore;
    public int baseMultiplier;
    public Sprite icon;
}
public enum PokerHand
{
    None,
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    //FiveOfAKind // custom như trong Balatro
}
