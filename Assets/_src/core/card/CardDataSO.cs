using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Balatro.Cards
{
    public enum CardType
    {
        StandardPoker,
        Joker,
        Special
    }

    [CreateAssetMenu(fileName = "CardDataSO", menuName = "Scriptable Objects/CardDataSO")]
    public class CardDataSO : ScriptableObject
    {
        [Header("Card Identification")]
        public int cardId;
        public string cardName;
        public CardType cardType = CardType.StandardPoker;

        [Header("Visual Properties")]
        public Sprite cardSprite;
        public Color primaryColor = Color.white;
        public Color secondaryColor = Color.black;

        [Header("Poker Properties")]
        public CardSuit suit;
        public CardRank rank;
        public int value => (int)rank < 10 ? (int)rank : rank == CardRank.Ace ? 11 : 10;

        [Header("Gameplay Properties")]
        [TextArea(3, 10)]
        public string cardDescription;
        public int baseScore;

        [Header("Special Abilities")]
        public bool hasSpecialAbility;
        [TextArea(3, 10)]
        public string specialAbilityDescription;
#if UNITY_EDITOR
        private void GetData()
        {
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
            this.cardName = $"{rank.ToString()} {suit.ToString()}".ToUpper();
            this.cardId = HashCode.Combine(suit, rank);

            // Tải sprite từ Resources
            int _rank = (int)rank == 14 ? 1 : (int)rank;
            cardSprite = Resources.Load<Sprite>($"Playing Cards/card-{suit.ToString().ToLower()}-{_rank}");

        }

        // OnEnable để tự động gọi GetData khi ScriptableObject được tải


        protected void OnValidate()
        {
            GetData();
        }
#endif
    }
    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
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
}

