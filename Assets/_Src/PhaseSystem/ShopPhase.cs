namespace PhaseSystem
{
    public class ShopPhase : BasePhase
    {
        public ShopPhase(PhaseManager manager) : base(manager)
        {
        }
        public override void Enter()
        {
            base.Enter();
            UIManager.Instance.ShowUIForPhase(this);
        }
    }

}
