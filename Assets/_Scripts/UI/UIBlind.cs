using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.InputManagerEntry;

public class UIBlind : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI btn_txt;
    [SerializeField] private TextMeshProUGUI blind_name;
    [SerializeField] private TextMeshProUGUI blind_score;
    [SerializeField] private TextMeshProUGUI reward;

    private Blind blind;
    public bool isDefeat;
    public bool isSkipped;
    public bool isLocked;

    public void Initlize(Blind blind, int baseChips)
    {
        isDefeat = false;
        isSkipped = false;
        isLocked = true;

        this.blind = blind;
        btn_txt.text = "Locked";
        blind_name.text = blind.name;
        blind_score.text = (int) (baseChips * blind.score_multiple) + "";
        reward.text = "Reward: $" + blind.reward;
        btn.onClick.RemoveAllListeners();
        
    }
    public void UnLock()
    {
        isLocked = true;
        btn_txt.text = "Select";
        btn.onClick.AddListener(() => GameManager.instance.SetBlind(blind));
        btn.onClick.AddListener(() => GameManager.instance.ChangePhase(Phase.playing));

    }
    public void Defeated()
    {
        isDefeat = true;
        btn_txt.text = "Defeated";
        btn.onClick.RemoveAllListeners();

    }

    public void Skipped()
    {
        isSkipped = true;
        btn_txt.text = "Skipped";
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
