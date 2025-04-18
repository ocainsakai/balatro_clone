using UniRx;

namespace Game.States
{
    public class StateMachine
    {
        private readonly ReactiveProperty<IGameState> currentState = new ReactiveProperty<IGameState>();

        public IReadOnlyReactiveProperty<IGameState> CurrentState => currentState;

        public void ChangeState(IGameState newState)
        {
            currentState.Value?.Exit();
            currentState.Value = newState;
            currentState.Value?.Enter();
        }
    }

    public interface IGameState
    {
        void Enter();
        void Exit();
    }

}
