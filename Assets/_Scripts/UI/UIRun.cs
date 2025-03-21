using TMPro;
using UnityEngine;

public class UIRun : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI round_score;
    [SerializeField] private TextMeshProUGUI hands;
    [SerializeField] private TextMeshProUGUI discards;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI ante;
    [SerializeField] private TextMeshProUGUI round;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.OnRunUpdate += UpdateRunUI;
    }

    public void UpdateRunUI(GameManager.Run run)
    {
        round_score.text = run.round_score + "";
        hands.text = run.play_hands + "";
        discards.text = run.discards +"";
        money.text = run.money +"";
        ante.text = run.ante + "";
        round.text = run.round + "";
    }
}
