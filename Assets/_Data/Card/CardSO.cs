using NUnit.Framework.Internal.Builders;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public int value;
    public Suit suit;
    public Sprite artWork;


    private void OnValidate()
    {
        cardName = this.name;
        ParseNameForValueAndSuit();

    }
    private void ParseNameForValueAndSuit()
    {
        string pattern = @"(?<value>\d+|ace|two|three|four|five|six|seven|eight|nine|ten|king|queen|jack)_(?<suit>\w+)";
        Match match = Regex.Match(name, pattern);

        if (match.Success)
        {
            string valueStr = match.Groups["value"].Value;
            if (int.TryParse(valueStr, out int parsedValue))
            {
                value = parsedValue;
            }
            else
            {
                value = valueStr switch
                {
                    "ace" => 1,
                    "two" => 2,
                    "three" => 3,
                    "four" => 4,
                    "five" => 5,
                    "six" => 6,
                    "seven" => 7,
                    "eight" => 8,
                    "nine" => 9,
                    "ten" => 10,
                    "jack" => 11,
                    "queen" => 12,
                    "king" => 13,
                    _ => value
                };
            }

            if (System.Enum.TryParse(match.Groups["suit"].Value, true, out Suit parsedSuit))
            {
                suit = parsedSuit;
            }
        }
    }
}

public enum Suit
{
    heart,
    diamond,
    club,
    spade
}

