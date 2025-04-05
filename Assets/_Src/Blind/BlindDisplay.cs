using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlindDisplay : MonoBehaviour
{
    [SerializeField] Button selectBtn;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI blind_name;
    [SerializeField] TextMeshProUGUI blind_score;
    [SerializeField] BlindRuntimeSO blind;

    private void OnEnable()
    {
        blind.onBlindChange += UpdateBlindDisplay;
    }
    private void OnDisable()
    {
        blind.onBlindChange -= UpdateBlindDisplay;
    }
    public void UpdateBlindDisplay(IBlind blind)
    {
        icon.sprite = blind.Sprite;
        blind_name.text = blind.Name;
        blind_score.text = $"Score at least: {blind.BlindScore}";
    }
}
