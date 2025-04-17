using PhaseSystem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BetPhaseUI : BasePhaseUI
{
    [SerializeField] GameObject betPanel;
    //public override Type Phase => typeof(BetPhase);
    public void UpdateBetPanel(List<BetOption> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            var option = options[i];
            var optionUI = betPanel.transform.GetChild(i);
            var optionText = optionUI.GetComponentInChildren<TextMeshProUGUI>();
            optionText.text = $"{option.betName}\n" +
                            $"Blind: {option.requiredScore}\n" +
                            $"Reward: $3";
        }
    }
    public override void Hide()
    {
       betPanel.SetActive(false);
    }

    public override void Show()
    {
        Debug.Log("bet ui show");
        betPanel.SetActive(true);
    }
}
