using PhaseSystem;
using System;
using UnityEngine;

public class PlayUI : BasePhaseUI
{
    [SerializeField] GameObject actionBtns;
    [SerializeField] GameObject hud;

    public override Type Phase => typeof(PlayPhase);

    public override void Hide()
    {
        //Debug.Log("play hide");
        actionBtns.SetActive(false);
        hud.SetActive(false);
    }
    public override void Show()
    {
        hud.SetActive(true);
        actionBtns.SetActive(true);
    }
}
