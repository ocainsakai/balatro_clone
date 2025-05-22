using UnityEngine;

[CreateAssetMenu(fileName = "PokerData", menuName = "Scriptable Objects/PokerData")]
public class PokerData : ScriptableObject
{
    public PokerHandType handType;
    public int Chip;
    public int Mult;
    public string PokerName => handType.ToString().AddWhiteSpace();

    [ContextMenu("Name")]
    void Show()
    {
        Debug.Log(PokerName);
    }
}
public enum PokerHandType
{
    PokerType,
    // Standard hands
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,

    // Secret hands
    FiveOfAKind,
    FlushHouse,
    FlushFive
}
