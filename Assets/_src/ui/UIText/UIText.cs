using TMPro;
using UnityEngine;

namespace Balatro.UI
{
    public class UIText : MonoBehaviour
    {
        TextMeshProUGUI _text => GetComponent<TextMeshProUGUI>();
        protected virtual void UpdateText(string text)
        {

        }
        protected virtual void UpdateInt(int number)
        {

        }
    }

}
