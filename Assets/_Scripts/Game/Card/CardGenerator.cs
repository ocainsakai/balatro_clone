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
    

    /// <summary>
    /// Generate and save a full standard deck (52 cards).
    /// </summary>
    /// pu
    public static void RegenerateGUID()
    {
        string[] guids = AssetDatabase.FindAssets("t:CardData", new[] { folderPath });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CardData card = AssetDatabase.LoadAssetAtPath<CardData>(path);

            if (card != null)
            {
                card.RegenerateGuid(); 
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();


    }
    public static void GenerateDeck()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }


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
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
