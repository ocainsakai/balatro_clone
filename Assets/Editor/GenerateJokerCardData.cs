using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GenerateJokerCardData : EditorWindow
{
    [MenuItem("Tools/Generate Joker")]
    public static void ShowWindow()
    {
        GetWindow<GenerateJokerCardData>("Joker Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Joker Generator", EditorStyles.boldLabel);
        if (GUILayout.Button("Generate Joker"))
        {
            GenerateJoker();
        }
    }
    public static string JokerSpritesPath = "Assets/Resources/Art/joker_images";
    public static string JokerDataPath = "Assets/Resources/GameData/Joker";
    public static string JokerJsonPath = "Assets/Resources/GameData/balatro_jokers_detailed.json";
    [Serializable]
    public class JokerJsonData
    {
        public string name;
        public string effect;
        public string cost;
        public string rarity;
        public string unlock_requirement;
        public string type;
        public string act;
        public int base_price;
    }
    [Serializable]
    public class JokerJsonList
    {
        public List<JokerJsonData> jokers;
    }
    private void GenerateJoker()
    {
        if (!Directory.Exists(JokerDataPath))
        {
            Directory.CreateDirectory(JokerDataPath);
        }
        string[] spriteGuids = AssetDatabase.FindAssets("t:Sprite", new[] { JokerSpritesPath });
        if (spriteGuids.Length == 0)
        {
            Debug.LogWarning($"No sprites found in '{JokerSpritesPath}'. Please ensure your sprites are in this folder.");
            return;
        }

        foreach (string guid in spriteGuids)
        {
            string spritePath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite jokerSprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
            if (jokerSprite != null)
            {
                string jokerName = Path.GetFileNameWithoutExtension(spritePath);
                JokerCardData newJoker = ScriptableObject.CreateInstance<JokerCardData>();
                newJoker.JokerName = jokerName;
                newJoker.Art = jokerSprite;

                string assetName = jokerName;
                string assetPath = Path.Combine(JokerDataPath, assetName + ".asset");
                assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
                AssetDatabase.CreateAsset(newJoker, assetPath);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Update Jokers From JSON")]
    public static void UpdateJokersFromJson()
    {
        if (!File.Exists(JokerJsonPath))
        {
            Debug.LogError($"JSON file not found at: {JokerJsonPath}");
            return;
        }

        string jsonText = File.ReadAllText(JokerJsonPath);
        List<JokerJsonData> jokerList = JsonUtility.FromJson<JokerJsonList>("{\"jokers\":" + jsonText + "}").jokers;

        Dictionary<string, JokerJsonData> jsonDataByName = new();
        foreach (var j in jokerList)
        {
            jsonDataByName[j.name.ToLowerInvariant()] = j;
        }

        string[] assetGuids = AssetDatabase.FindAssets("t:JokerCardData", new[] { JokerDataPath });

        int updatedCount = 0;
        foreach (string guid in assetGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var jokerAsset = AssetDatabase.LoadAssetAtPath<JokerCardData>(path);

            if (jokerAsset == null) continue;

            string nameKey = jokerAsset.JokerName.ToLowerInvariant();

            if (jsonDataByName.TryGetValue(nameKey, out var json))
            {
                jokerAsset.Description = json.effect;
                jokerAsset.Price = json.base_price;
                
                //jokerAsset.Effect = json.effect;
                //jokerAsset.BasePrice = json.base_price;
                //jokerAsset.Rarity = json.rarity;
                //jokerAsset.UnlockRequirement = json.unlock_requirement;
                //jokerAsset.Type = json.type;
                //jokerAsset.Act = json.act;

                EditorUtility.SetDirty(jokerAsset);
                updatedCount++;
            }
            else
            {
                Debug.LogWarning($"No JSON data found for asset: {jokerAsset.JokerName}");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Updated {updatedCount} existing JokerCardData assets from JSON.");
    }

}
