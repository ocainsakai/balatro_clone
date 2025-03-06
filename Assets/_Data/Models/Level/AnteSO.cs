using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "AnteSO", menuName = "Scriptable Objects/AnteSO")]
public class AnteSO : ScriptableObject
{
    public int ante;
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
                ante = parsedValue;
                targetScore = 30 * (int)Mathf.Pow(2, ante-1);
            }
        }
    }
}
