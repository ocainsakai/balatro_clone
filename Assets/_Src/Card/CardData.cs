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
            _cardName = this.name;
            _cardID = this.GetHashCode().ToString();
            var token = name.Split(' ');
            if (token.Length > 1)
            {
                Suit = Enum.TryParse<CardSuit>(token[0], out Suit) ? Suit : CardSuit.Hearts;
                int _rank = int.Parse(token[1]);
                Rank = _rank == 1 ? CardRank.Ace : (CardRank)_rank;
                _image = Resources.Load<Sprite>($"Art/PNG/Card-{token[0]}-{_rank}");

            }
        }
    }
#endif
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