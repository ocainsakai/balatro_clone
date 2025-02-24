using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    private TextMeshProUGUI tmp => GetComponent<TextMeshProUGUI>();
    public void UpdateData(object data)
    {
        tmp.text = $"{name} {data}";
    }

}
