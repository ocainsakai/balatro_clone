using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    protected TextMeshProUGUI text => GetComponentInChildren<TextMeshProUGUI>();
    protected Button button => GetComponent<Button>();
    protected void Reset()
    {
        if(text != null)
        {
            text.text = this.name;
        }
    }
}
