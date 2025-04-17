
namespace PhaseSystem
{
    public class BetPhase : BasePhase
    {
        public BetPhase(PhaseManager manager) : base(manager)
        {
        }
        public override void Enter()
        {
            base.Enter();
            uiManager.ShowUIForPhase<BetPhase>();       
        }
    }

}
