using UnityEngine;
using System.Collections.Generic;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;

namespace Game.Cards
{
#endif

    /// <summary>
    /// Utility class to generate a full deck of CardData assets.
    /// </summary>
    public static class CardGenerator
    {
        public static string folderPath = "Assets/GameData/Deck";
        public static List<CardData> generatedDeck;

        /// <summary>
        /// Generate and save a full standard deck (52 cards).
        /// </summary>
        public static void GenerateDeck()
        {
#if UNITY_EDITOR
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            generatedDeck = new List<CardData>();

            for (int suit = CardSuit.Hearts; suit <= CardSuit.Spades; suit++)
            {
                for (int rank = CardRank.Two; rank <= CardRank.Ace; rank++)
                {
                    // Create new CardData ScriptableObject
                    CardData newCard = ScriptableObject.CreateInstance<CardData>();

                    // Setup internal name
                    string rankName = GetRankName(rank);
                    string suitName = GetSuitName(suit);
                    string assetName = $"{rankName}_{suitName}";

                    // Set data fields
                    newCard.name = assetName;
                    newCard.Rank = rank;
                    newCard.Suit = suit;

                    // Save as .asset file
                    string assetPath = Path.Combine(folderPath, assetName + ".asset");
                    AssetDatabase.CreateAsset(newCard, assetPath);

                    generatedDeck.Add(newCard);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"Deck generated with {generatedDeck.Count} cards.");
#else
        Debug.LogError("GenerateDeck can only be called inside Unity Editor.");
#endif
        }

        /// <summary>
        /// Print all generated cards.
        /// </summary>
        public static void PrintDeck()
        {
            if (generatedDeck == null)
            {
                Debug.LogWarning("No deck generated yet.");
                return;
            }

            foreach (CardData card in generatedDeck)
            {
                Debug.Log($"{card.Rank} of {card.Suit}");
            }
        }

        private static string GetRankName(int rank)
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

        private static string GetSuitName(int suit)
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
    }
}