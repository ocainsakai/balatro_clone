using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    [Header("Card Identity")]
    public string cardId;
    public string cardName;

    [Header("Card Visuals")]
    public Sprite cardFrontImage;
    public Color cardColor = Color.white;

    [Header("Card Properties")]
    public CardSuit suit;
    public CardRank rank;
    public int baseValue => IsRoyal() ? rank == CardRank.Ace ? 11 : 10 : (int)rank;
    public CardType cardType = CardType.Standard;

    [Header("Card Effects")]
    [TextArea(3, 5)]
    public string cardDescription;

    [Header("Metadata")]
    public bool isSpecialCard;
    public int rarityTier; // 0 = common, 1 = uncommon, 2 = rare, 3 = legendary
#if UNITY_EDITOR
    private void GetData()
    {
        this.cardName = this.name;
        string pattern = @"(?<suit>\w+) (?<value>\d+|ace|two|three|four|five|six|seven|eight|nine|ten|king|queen|jack)";
        Match match = Regex.Match(name, pattern);
        if (match.Success)
        {
            string valueStr = match.Groups["value"].Value;
            if (int.TryParse(valueStr, out int parsedValue))
            {
                rank = (CardRank)parsedValue; // Chuyển đổi int thành enum CardRank
            }
            else
            {
                rank = valueStr switch
                {
                    "ace" => CardRank.Ace,
                    "two" => CardRank.Two,
                    "three" => CardRank.Three,
                    "four" => CardRank.Four,
                    "five" => CardRank.Five,
                    "six" => CardRank.Six,
                    "seven" => CardRank.Seven,
                    "eight" => CardRank.Eight,
                    "nine" => CardRank.Nine,
                    "ten" => CardRank.Ten,
                    "jack" => CardRank.Jack,
                    "queen" => CardRank.Queen,
                    "king" => CardRank.King,
                    "1" => CardRank.Ace,
                    _ => rank
                };
            }

            // Xử lý suit từ match
            string suitStr = match.Groups["suit"].Value.ToLower();
            suit = suitStr switch
            {
                "hearts" => CardSuit.Hearts,
                "diamonds" => CardSuit.Diamonds,
                "clubs" => CardSuit.Clubs,
                "spades" => CardSuit.Spades,
                _ => suit
            };
        }

        // Tải sprite từ Resources
        int _rank = rank == CardRank.Ace ? 1 : (int) rank;
        cardFrontImage = Resources.Load<Sprite>($"PNG" +
            $"/card-{suit.ToString().ToLower()}-{_rank}");

    }

    // OnEnable để tự động gọi GetData khi ScriptableObject được tải


    protected void OnValidate()
    {
        GetData();
    }
#endif

    public bool IsRoyal()
    {
        return new List<CardRank>() { CardRank.Ace, CardRank.Jack, CardRank.Queen, CardRank.King }.Contains(rank);
    }
    // Enums
    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades,
        Joker,
        Special
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
        Joker = 0
    }

    public enum CardType
    {
        Standard,   // Thẻ bài thông thường
        Tarot,      // Thẻ tarot có tác động lớn
        Planet,     // Thẻ hành tinh
        Joker,      // Joker với hiệu ứng đặc biệt
        Voucher,    // Phiếu ưu đãi
        Spectral,   // Thẻ ma
        Enhancement // Thẻ nâng cấp
    }

    // Phương thức lấy giá trị hiển thị của thẻ
    public string GetDisplayRank()
    {
        if (cardType != CardType.Standard && cardType != CardType.Joker)
            return "";

        switch (rank)
        {
            case CardRank.Ace: return "A";
            case CardRank.Jack: return "J";
            case CardRank.Queen: return "Q";
            case CardRank.King: return "K";
            case CardRank.Joker: return "JOKER";
            default: return ((int)rank).ToString();
        }
    }

    // Lấy biểu tượng suit
    public string GetSuitSymbol()
    {
        switch (suit)
        {
            case CardSuit.Hearts: return "♥";
            case CardSuit.Diamonds: return "♦";
            case CardSuit.Clubs: return "♣";
            case CardSuit.Spades: return "♠";
            default: return "";
        }
    }

    // Lấy màu của thẻ
    public Color GetSuitColor()
    {
        switch (suit)
        {
            case CardSuit.Hearts:
            case CardSuit.Diamonds:
                return new Color(0.9f, 0.2f, 0.2f);
            case CardSuit.Clubs:
            case CardSuit.Spades:
                return new Color(0.2f, 0.2f, 0.2f);
            case CardSuit.Joker:
                return new Color(0.9f, 0.1f, 0.9f);
            default:
                return Color.black;
        }
    }
}