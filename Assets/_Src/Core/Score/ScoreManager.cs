using Balatro.Cards.System;
using PhaseSystem;
using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : BaseManager
{
    [Header("RSO")]
    [SerializeField] IntRSO total_score;
    [SerializeField] IntRSO require_score;
    [SerializeField] ComboTypeRSO comboType;

    //public static event Action CompleteScore;

    public void Score()
    {
        BaseSelectScore();
        CheckBlind();
    }

    void CheckBlind()
    {
        if (total_score.Value >= require_score.Value)
        {
            GetManager<PhaseManager>().GoToShopPhase();
        }
        else
        {
            GetManager<HandManager>().Discard();
        }
    }
    void BaseSelectScore()
    {
        var selected = GetManager<HandManager>().GetSelected();
        foreach (var card in selected) {
            if (comboType.comboCard.Contains(card))
            {
                comboType.chip.Value += card.GetValue();
                card.CardView.OnScore();
            }
        }
        int amount = comboType.chip.Value * comboType.mult.Value;
        total_score.Value += amount;
    }
}
