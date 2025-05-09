#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Game.Cards;

public static class CardGenerator
{
#if UNITY_EDITOR
    public static string folderPath = "Assets/GameData/Deck";
    public static List<CardData> generatedDeck;

    /// <summary>
    /// Generate and save a full standard deck (52 cards).
    /// </summary>
    public static void GenerateDeck()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        generatedDeck = new List<CardData>();

        foreach (CardSuit suit in System.Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardRank rank in System.Enum.GetValues(typeof(CardRank)))
            {
                // Create new CardData ScriptableObject
                CardData newCard = ScriptableObject.CreateInstance<CardData>();

                // Setup internal name
                string rankName = rank.ToString();
                string suitName = suit.ToString();
                string assetName = $"{rankName}_{suitName}";

                // Set data fields
                newCard.name = assetName;
                newCard.Rank = rank;
                newCard.Suit = suit;

                // Save as .asset file (ensure no name conflict)
                string assetPath = Path.Combine(folderPath, assetName + ".asset");
                assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

                AssetDatabase.CreateAsset(newCard, assetPath);
                generatedDeck.Add(newCard);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Deck generated with {generatedDeck.Count} cards.");
    }
#endif
}
