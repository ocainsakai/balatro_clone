using Game.Managers;
using Game.Services;
using Zenject;

namespace Game.States {
    public class GameOverState : IGameState
    {
        private readonly UIService uiService;

        [Inject]
        public GameOverState(UIService uiService)
        {
            this.uiService = uiService;
        }

        public void Enter()
        {
            //uiService.ShowMainMenu();
        }

        public void Exit()
        {
            //uiService.HideMainMenu();
        }
    }

}

