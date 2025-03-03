using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    private TextMeshProUGUI tmp => GetComponent<TextMeshProUGUI>();
    private void Reset()
    {
        this.tmp.text = name;
    }
    public void UpdateData(object data)
    {
        tmp.text = $"{data}";
    }

}
