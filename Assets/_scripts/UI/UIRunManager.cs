
using TMPro;
using UnityEngine;


public class UIRunManager : SingletonAbs<UIRunManager>
{
    [SerializeField] private TextMeshProUGUI round_score;
    [SerializeField] private TextMeshProUGUI hands;
    [SerializeField] private TextMeshProUGUI discards;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI ante;
    [SerializeField] private TextMeshProUGUI round;
    //[SerializeField] UIPokerHand ui_poker_hand;


    public void UpdateRunUI(GameManager.Run run)
    {
        round_score.text = run.round_score.ToString();
        hands.text = run.play_hands + "";
        discards.text = run.discards + "";
        money.text = run.money + "";
        ante.text = run.ante + "";
        round.text = run.round + "";
    }
    //public IEnumerator AddScore(int amount)
    //{
    //    yield return (PlusNumber(round_score, amount, 0.2f));
    //}
}