using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("RSO")]
    [SerializeField] IntRSO total_score;
    [SerializeField] ComboTypeRSO comboType;

    public static event Action CompleteScore;

    public void Score()
    {
        StartCoroutine(BaseSelectScore());
        CompleteScore?.Invoke();
    }
    IEnumerator BaseSelectScore()
    {
        foreach (var card in comboType.comboCard) {
            comboType.chip.Value += card.GetValue();
            card.CardView.OnScore();
            yield return new WaitForSeconds(0.2f);
        }
        int amount = comboType.chip.Value * comboType.mult.Value;
        total_score.Value += amount;
    }
}
