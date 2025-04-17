namespace PhaseSystem
{
    using UnityEngine;

    public class PhaseManager : MonoBehaviour, IManager
    {
        private IPhase currentPhase;

        public void Start()
        {
            Initialize();
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

        public void Initialize()
        {
            ChangePhase(new BetPhase(this));

        }

        public void ResetManager()
        {
            //throw new System.NotImplementedException();
        }

        public void Cleanup()
        {
            //throw new System.NotImplementedException();
        }
    }

}
