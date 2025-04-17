using UnityEngine;

namespace PhaseSystem
{
    public class PlayPhase : BasePhase
    {

        public PlayPhase(PhaseManager manager) : base(manager)
        {
        }
        public override void Enter()
        {
            base.Enter();
            uiManager.ShowUIForPhase<PlayUI>();

        }
    }
}

