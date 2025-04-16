namespace PhaseSystem
{
    public abstract class BasePhase : IPhase
    {
        protected PhaseManager manager;

        public BasePhase(PhaseManager manager)
        {
            this.manager = manager;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
    }

}