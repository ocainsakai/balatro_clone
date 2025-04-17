

using PhaseSystem;
using System.Diagnostics;

public class UIManager : BaseManager
{

    public void ShowUIForPhase<T>() where T : BasePhaseUI
    {
        UnityEngine.Debug.Log("show ui manager");
        foreach (BasePhaseUI ui in BasePhaseUI.Instances)
        {
            ui.Turn(ui is T);
        }
    }

}
