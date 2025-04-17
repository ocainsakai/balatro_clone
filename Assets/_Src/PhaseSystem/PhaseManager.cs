namespace PhaseSystem
{

    public class PhaseManager : BaseManager
    {
        private IPhase currentPhase;

        public void ChangePhase(IPhase newPhase)
        {
            currentPhase?.Exit();
            currentPhase = newPhase;
            currentPhase.Enter();
        }

        // Call this to go to the next phase (for example)
        public void GoToPlayPhase()
        {
            ChangePhase(new PlayPhase(this));
        }

        public void GoToShopPhase()
        {
            ChangePhase(new ShopPhase(this));
        }

        public override void Initialize()
        {
            ChangePhase(new BetPhase(this));

        }

    }

}
