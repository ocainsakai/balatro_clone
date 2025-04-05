using System.Text.RegularExpressions;
using UnityEngine;

public static class Utilities
{
    public static string AddSpaces(string input)
    {
        return Regex.Replace(input, @"(\p{Lu})", " $1").Trim();
    }
}
