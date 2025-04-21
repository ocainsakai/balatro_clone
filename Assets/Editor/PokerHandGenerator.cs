using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public class PokerHandGenerator : EditorWindow
{
    [MenuItem("Tools/Poker/Generate All PokerHandData")]
    public static void Generate()
    {
        string path = "Assets/GameData/PokerHands/";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var handTypes = System.Enum.GetValues(typeof(PokerHand)).Cast<PokerHand>();

        foreach (var hand in handTypes)
        {
            string assetPath = path + hand.ToString() + ".asset";

            if (File.Exists(assetPath)) continue;

            var data = ScriptableObject.CreateInstance<PokerHandData>();
            data.handType = hand;
            data.displayName = hand.ToString();
            data.description = $"This is a {hand} hand.";
            data.baseScore = 100;
            data.baseMultiplier = 1;

            AssetDatabase.CreateAsset(data, assetPath);
            Debug.Log($"Created: {hand}");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
