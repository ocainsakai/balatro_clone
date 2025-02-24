using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<UIText> texts;

    public void UpdateText(string target, object data)
    {
        UIText textToUpdate = texts.FirstOrDefault(t =>
        t.name.IndexOf(target, StringComparison.OrdinalIgnoreCase) >= 0);
        textToUpdate.UpdateData(data);
    }
}
