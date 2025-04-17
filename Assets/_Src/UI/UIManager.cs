

using PhaseSystem;

public class UIManager : BaseManager
{

    public void ShowUIForPhase<T>() where T : BasePhase
    {
        
        foreach (BasePhaseUI ui in BasePhaseUI.Instances)
        {
            ui.Turn(ui is T);
        }
    }

}
