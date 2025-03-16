using TMPro;
using UnityEngine;

public class RunUI : MonoBehaviour
{
    public TextMeshProUGUI phaseInfor;
    public TextMeshProUGUI roundScore;
    public TextMeshProUGUI round;
    public TextMeshProUGUI ante;
    public TextMeshProUGUI money;
    public TextMeshProUGUI handCount;
    public TextMeshProUGUI discardCount;
    public void Start()
    {
        UpdateRun(GameManager.Instance.runManager.run);
    }
    public void UpdateRun(Run run)
    {
        roundScore.text = run.roundScore.ToString();
        round.text = run.round.ToString();
        ante.text = run.ante.ToString();
        handCount.text = run.handCount.ToString();
        discardCount.text = run.discardCount.ToString();
        money.text = run.money.ToString();
    }
}
