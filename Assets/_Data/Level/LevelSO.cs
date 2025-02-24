using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    public int level;
    public int targetScore;
    private void OnValidate()
    {
        
        ParseName();

    }
    private void ParseName()
    {
        string pattern = @"(Level)_(?<number>\d+)";
        Match match = Regex.Match(name, pattern);
        if (match.Success)
        {
            string valueStr = match.Groups["number"].Value;
            if (int.TryParse(valueStr, out int parsedValue))
            {
                level = parsedValue;
                targetScore = 300 * (int)Mathf.Pow(2, level-1);
            }
        }
    }
}
