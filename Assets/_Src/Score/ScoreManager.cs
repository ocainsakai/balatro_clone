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
        StartCoroutine( BaseSelectScore());
        CompleteScore?.Invoke();
    }
    IEnumerator BaseSelectScore()
    {
        foreach (var item in comboType.comboCard) {
            comboType.chip.Value += item.GetValue();
            yield return 0.2f;
        }
        int amount = comboType.chip.Value * comboType.mult.Value;
        total_score.Value += amount;
    }
}
