using TMPro;
using UnityEngine;

public class RunUI : MonoBehaviour
{
    public TextMeshProUGUI roundScore;
    public TextMeshProUGUI round;
    public TextMeshProUGUI ante;
    public TextMeshProUGUI money;
    public TextMeshProUGUI handCount;
    public TextMeshProUGUI discardsCount;
    public void Start()
    {
        UpdateRun(GameManager.Instance.running);
    }
    public void UpdateRun(Run run)
    {
        roundScore.text = run.totalScore.ToString();
        round.text = run.round.ToString();
        ante.text = run.anteLevel.ToString();
        handCount.text = run.handCount.ToString();
        discardsCount.text = run.discardsCount.ToString();
        money.text = run.money.ToString();
    }
}
