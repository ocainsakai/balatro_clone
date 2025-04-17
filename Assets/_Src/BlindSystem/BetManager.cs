using System.Collections.Generic;
using UnityEngine;

public class BetManager : BaseManager
{
    public List<BetOption> currentBets { get; private set; }
    [SerializeField] BetDatabase database;

    BetPhaseUI betUI => BasePhaseUI.GetPhaseUI<BetPhaseUI>();
    public void LoadBetOptions(int difficulty)
    {
        currentBets = new List<BetOption>();
        currentBets.Add(database.options[0]);
        currentBets.Add(database.options[1]);
        currentBets.Add(database.options[2]);

        betUI.UpdateBetPanel(currentBets);
    }
}
