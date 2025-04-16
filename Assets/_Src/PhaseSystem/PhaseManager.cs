namespace PhaseSystem
{
    using UnityEngine;

    public class PhaseManager : MonoBehaviour
    {
        private IPhase currentPhase;

        public void Start()
        {
            // Start with the BetPhase
            ChangePhase(new BetPhase(this));
        }

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
    }

}
