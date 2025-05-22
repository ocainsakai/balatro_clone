using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class GeneratePokerData : EditorWindow
{
    [MenuItem("Tools/Generate Poker")]
    public static void ShowWindow()
    {
        GetWindow<GeneratePokerData>("Card Generator");
    }
    private void OnGUI()
    {
        GUILayout.Label("Poker Generator", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate All PokerData"))
        {
            GeneratePokerAssets();
        }
    }
    public static string folderPath = "Assets/Resources/GameData/Poker";

    private static void GeneratePokerAssets()
    {
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            AssetDatabase.Refresh();
        }

        List<(PokerHandType type, int chip, int mult)> pokerHandConfigs = new()
        {
            (PokerHandType.PokerType, 0, 0),
            (PokerHandType.HighCard,      5,   1),
            (PokerHandType.Pair,          10,  2),
            (PokerHandType.TwoPair,       20,  2),
            (PokerHandType.ThreeOfAKind,  30,  3),
            (PokerHandType.Straight,      30,  4),
            (PokerHandType.Flush,         35,  4),
            (PokerHandType.FullHouse,     40,  4),
            (PokerHandType.FourOfAKind,   60,  7),
            (PokerHandType.StraightFlush, 100, 8),
            (PokerHandType.FiveOfAKind,   120, 12),
            (PokerHandType.FlushHouse,    140, 14),
            (PokerHandType.FlushFive,     160, 16)
        };

        foreach (var config in pokerHandConfigs)
        {
            PokerData pokerAsset = CreatePokerData(config.type, config.chip, config.mult);

            string path = $"{folderPath}/{pokerAsset.handType}.asset";
            AssetDatabase.CreateAsset(pokerAsset, path);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("PokerData assets generated successfully.");
    }

    private static PokerData CreatePokerData(PokerHandType handType, int chip, int mult)
    {
        PokerData asset = ScriptableObject.CreateInstance<PokerData>();
        asset.handType = handType;
        asset.Chip = chip;
        asset.Mult = mult;
        return asset;
    }
}
