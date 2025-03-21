using System;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public Phase currentPhase { get; private set; }
    public event Action<string> OnPhaseChanged;

    public void ChangePhase(Phase newPhase)
    {
        currentPhase = newPhase;
        switch (currentPhase)
        {
            case Phase.blind:
                GameManager.instance.StartChoosePhase();
                break;
            case Phase.playing:
                GameManager.instance.StartPlayingPhase();
                break;
            case Phase.shop:
                GameManager.instance.StartShopPhase();
                break;
            case Phase.gameover:
                GameManager.instance.StartGameOverPhase();
                break;
        }
        OnPhaseChanged?.Invoke($"Phase: {currentPhase}");
    }
}