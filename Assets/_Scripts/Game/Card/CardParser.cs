using System;
using UnityEngine;

namespace Game.Cards
{
    public static class CardParser
    {
        public static int ParseRank(string name)
        {
            foreach (var word in name.Split('_', StringSplitOptions.RemoveEmptyEntries))
            {

                if (int.TryParse(word, out int number))
                {
                    if (number >= 2 && number <= 14)
                        return number;
                }
                switch (word)
                {
                    case "LowAce": return CardRank.LowAce;
                    case "Two": return CardRank.Two;
                    case "Three": return CardRank.Three;
                    case "Four": return CardRank.Four;
                    case "Five": return CardRank.Five;
                    case "Six": return CardRank.Six;
                    case "Seven": return CardRank.Seven;
                    case "Eight": return CardRank.Eight;
                    case "Nine": return CardRank.Nine;
                    case "Ten": return CardRank.Ten;
                    case "Jack": return CardRank.Jack;
                    case "Queen": return CardRank.Queen;
                    case "King": return CardRank.King;
                    case "Ace": return CardRank.Ace;
                }
            }
            Debug.LogWarning($"Cannot parse rank from {name}, defaulting to Two");
            return CardRank.Two;
        }

        public static int ParseSuit(string name)
        {
            foreach (var word in name.Split('_', StringSplitOptions.RemoveEmptyEntries))
            {
                switch (word)
                {
                    case "Hearts": return CardSuit.Hearts;
                    case "Diamonds": return CardSuit.Diamonds;
                    case "Clubs": return CardSuit.Clubs;
                    case "Spades": return CardSuit.Spades;
                }
            }
            Debug.LogWarning($"Cannot parse suit from {name}, defaulting to Hearts");
            return CardSuit.Hearts;
        }

        /// <summary>
        /// Get the suit name as string from suit integer.
        /// </summary>
        public static string GetSuitName(int suit)
        {
            switch (suit)
            {
                case CardSuit.Hearts: return "Hearts";
                case CardSuit.Diamonds: return "Diamonds";
                case CardSuit.Clubs: return "Clubs";
                case CardSuit.Spades: return "Spades";
                default: return "Unknown";
            }
        }

        /// <summary>
        /// Get the rank name as string from rank integer.
        /// </summary>
        public static string GetRankName(int rank)
        {
            switch (rank)
            {
                case CardRank.LowAce: return "LowAce";
                case CardRank.Two: return "Two";
                case CardRank.Three: return "Three";
                case CardRank.Four: return "Four";
                case CardRank.Five: return "Five";
                case CardRank.Six: return "Six";
                case CardRank.Seven: return "Seven";
                case CardRank.Eight: return "Eight";
                case CardRank.Nine: return "Nine";
                case CardRank.Ten: return "Ten";
                case CardRank.Jack: return "Jack";
                case CardRank.Queen: return "Queen";
                case CardRank.King: return "King";
                case CardRank.Ace: return "Ace";
                default: return "Unknown";
            }
        }
    }
}