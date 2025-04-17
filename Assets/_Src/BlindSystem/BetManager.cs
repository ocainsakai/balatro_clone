using PhaseSystem;
using System.Collections.Generic;
using UnityEngine;

public class BetManager : BaseManager
{
    public List<BetOption> currentBets { get; private set; }
    [SerializeField] BetDatabase database;
    [SerializeField] BetRSO currentBet;
    BetPhaseUI betUI => BasePhaseUI.GetPhaseUI<BetPhaseUI>();
    PhaseManager phaseManager => GetManager<PhaseManager>();

    public override void Initialize()
    {
        base.Initialize();
        betUI.OnBetPhaseClick += (betOption) =>
        {
            currentBet.Option = betOption;
            phaseManager.GoToPlayPhase();
        };
    }
    public void LoadBetOptions(int difficulty)
    {
        currentBets = new List<BetOption>();
        currentBets.Add(database.options[0]);
        currentBets.Add(database.options[1]);
        currentBets.Add(database.options[2]);

        betUI.UpdateBetPanel(currentBets);
    }
}
