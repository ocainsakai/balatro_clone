using System.Collections;

using TMPro;
using UnityEngine;


public class UIRun : UITextAbstract
{
    [SerializeField] private TextMeshProUGUI round_score;
    [SerializeField] private TextMeshProUGUI hands;
    [SerializeField] private TextMeshProUGUI discards;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI ante;
    [SerializeField] private TextMeshProUGUI round;
    

    public void UpdateRunUI(GameManager.Run run)
    {
        //JumpText(round_score,run.round_score.ToString(), 0.2f);
        hands.text = run.play_hands + "";
        discards.text = run.discards + "";
        money.text = run.money + "";
        ante.text = run.ante + "";
        round.text = run.round + "";
    }
    public IEnumerator AddScore(int amount)
    {
        yield return (PlusNumber(round_score, amount, 0.2f));
    }
}