using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlindUI : MonoBehaviour
{
    public BlindSO currentBlind { get; private set; }
    [SerializeField] private TextMeshProUGUI blindNameText;
    [SerializeField] private TextMeshProUGUI blindValueText;
    [SerializeField] private TextMeshProUGUI baseChipsText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Image blindIconImage;
    [SerializeField] private TextMeshProUGUI effectDescriptionText;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText => button.GetComponentInChildren<TextMeshProUGUI>();


    public void SetBlind(BlindSO blindSO, int baseChips = 300)
    {
        this.currentBlind = blindSO;
        UpdateBlindUI(baseChips);
        button.onClick.RemoveAllListeners();

        if (currentBlind.isLocked)
        {
            buttonText.text = "Locked";
        }
        else if (currentBlind.isSkipped)
        {
            buttonText.text = "Skipped";

        }
        else if (currentBlind.isDefeated)
        {
            buttonText.text = "Defeated";
        }
        else
        {
            buttonText.text = "Select";
            button.onClick.AddListener(() =>
            {
            GameManager.Instance.ChangePhase(Phase.Score);
            BlindManager.Instance.SetBlind(this.currentBlind);
            });

        }

    }
    public void UpdateBlindUI(int baseChips)
    {
        blindNameText.text = currentBlind.blindName;
        baseChipsText.text ="" + (int) (currentBlind.scoreMultiple * baseChips);
        rewardText.text = $"Reward: {"$".PadRight(currentBlind.reward, '$')}+";
        blindIconImage.sprite = currentBlind.blindIcon;
        effectDescriptionText.text = currentBlind.effectDescrition;
    }
}
