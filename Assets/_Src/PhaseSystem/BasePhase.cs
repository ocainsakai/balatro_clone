namespace PhaseSystem
{
    public abstract class BasePhase : IPhase
    {
        protected PhaseManager manager;
        protected UIManager uiManager => manager.GetManager<UIManager>();
        public BasePhase(PhaseManager manager)
        {
            this.manager = manager;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
    }

}