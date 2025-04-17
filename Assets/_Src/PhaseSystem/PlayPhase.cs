using Balatro.Cards.System;
using UnityEngine;

namespace PhaseSystem
{
    public class PlayPhase : BasePhase
    {

        public PlayPhase(PhaseManager manager) : base(manager)
        {
        }
        public override void Exit()
        {
            base.Exit();
            manager.GetManager<HandManager>().ClearHand();
            manager.GetManager<DeckManager>().ClearDeck();
        }
        public override void Enter()
        {
            base.Enter();
            uiManager.ShowUIForPhase<PlayUI>();
            manager.GetManager<HandManager>().Draw();
        }
    }
}

