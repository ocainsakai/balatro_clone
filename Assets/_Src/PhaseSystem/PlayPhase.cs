using Balatro.Cards.System;
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
            manager.GetManager<HandManager>().Draw();
        }
    }
}

