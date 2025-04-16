using PhaseSystem;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowUIForPhase(BasePhase phase)
    {
        
        foreach (BasePhaseUI ui in BasePhaseUI.Instances)
        {
            var type = ui.Phase;
            //Debug.Log("show ui " + phase.GetType());
            ui.Turn(phase.GetType() == type);
        }
    }
}
