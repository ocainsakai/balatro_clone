using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public string _cardID;
    public string _cardName;
    public string _cardDescription;
    public Sprite _image;
    public CardRank Rank;
    public CardSuit Suit;
#if UNITY_EDITOR
    void OnValidate()
    {
        if (!string.IsNullOrEmpty(this.name))
        {
            _cardID = this.GetHashCode().ToString();
            var token = name.Split(' ');
            if (token.Length > 1)
            {
                Suit = ParseSuit(name);
                Rank = ParseRank(name);
                int i = Rank == CardRank.Ace ? 1 : (int)Rank;
                _image = Resources.Load<Sprite>($"Art/PNG/Card-{Suit}-{i}");

            }
            _cardName = Rank + " Of " + Suit;
        }
    }
#endif
    //public static CardRank ParseRank(int rank)
    //{

    //}
    public static CardRank ParseRank(string name)
    {
        foreach (var word in name.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            if (int.TryParse(word, out int num))
            {
                if (Enum.IsDefined(typeof(CardRank), num))
                    return (CardRank)num;
            }
            if (Enum.TryParse<CardRank>(word, true, out var rank))
                return rank;
        }
        Debug.LogWarning($"Cannot parse rank from {name}, defaulting to Two");
        return CardRank.Two;
    }
    public static CardSuit ParseSuit(string name)
    {
        foreach (var word in name.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            if (Enum.TryParse<CardSuit>(word, true, out var suit))
                return suit;
        }
        Debug.LogWarning($"Cannot parse suit from {name}, defaulting to Hearts");

        return CardSuit.Hearts;
    }
}

public enum CardSuit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades,
}
public enum CardRank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14,
}