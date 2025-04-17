using UnityEngine;

public class TxtUltil
{
    public static string AddSpacesToSentence(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        var newText = System.Text.RegularExpressions.Regex.Replace(text, "(?<!^)([A-Z])", " $1");
        return newText.Trim();
    }
}
