using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MetaUI : MonoBehaviour
{
    public List<UIText> texts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Reset()
    {
        texts.AddRange(GetComponentsInChildren<UIText>());
    }

    public void UpdateText(string target, object data)
    {
        UIText textToUpdate = texts.FirstOrDefault(t =>
        t.name.IndexOf(target, StringComparison.OrdinalIgnoreCase) >= 0);
        if (textToUpdate) textToUpdate.UpdateData(data);
    }
}
