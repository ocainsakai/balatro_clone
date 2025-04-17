
namespace PhaseSystem
{
    public class BetPhase : BasePhase
    {
        BetManager betManager => manager.GetManager<BetManager>();
        public BetPhase(PhaseManager manager) : base(manager)
        {
        }
        public override void Enter()
        {
            UnityEngine.Debug.Log("bet phase  enter");
            base.Enter();
            uiManager.ShowUIForPhase<BetPhaseUI>();
            betManager.LoadBetOptions(1);
        }
    }

}
