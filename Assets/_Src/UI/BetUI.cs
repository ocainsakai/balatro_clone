using PhaseSystem;
using System;
using UnityEngine;

public class BetUI : BasePhaseUI
{
    [SerializeField] GameObject betPanel;
    public override Type Phase => typeof(BetPhase);

    public override void Hide()
    {
       betPanel.SetActive(false);
    }

    public override void Show()
    {
        betPanel.SetActive(true);
    }
}
