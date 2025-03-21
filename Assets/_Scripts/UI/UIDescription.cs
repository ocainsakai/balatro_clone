using TMPro;
using UnityEngine;

public class UIDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;

    public void UpdateText(string text)
    {
        description.text = text;    
    }
}
