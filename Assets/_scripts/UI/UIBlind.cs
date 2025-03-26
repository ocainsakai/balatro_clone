using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class UIBlind : MonoBehaviour
{
    public BlindSO blindSO;
    [SerializeField] private Button btn;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI btn_txt;
    [SerializeField] private TextMeshProUGUI blind_name;
    [SerializeField] private TextMeshProUGUI blind_score;
    [SerializeField] private TextMeshProUGUI reward;

    public bool isDefeat;
    public bool isSkipped;
    public bool isLocked;
    public void SetBlind( BlindSO blindSO , int baseChips)
    {
        isDefeat = false;
        isSkipped = false;
        isLocked = true;

        this.blindSO = blindSO;
        btn_txt.text = "Locked";
        icon.sprite = blindSO.blindIcon;
        blind_name.text = blindSO.name;
        blind_score.text = (int)(baseChips * blindSO.scoreMultiple) + "";
        reward.text = "Reward: $" + blindSO.reward;
        btn.onClick.RemoveAllListeners();
    }
    public void InChoosing()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 size = rectTransform.sizeDelta;
        size.y += 50f;
        rectTransform.sizeDelta = size;
        btn_txt.text = "Select";
        btn.onClick.AddListener( () => GameManager.instance.ChangePhase(Phase.Score));
    }
}
