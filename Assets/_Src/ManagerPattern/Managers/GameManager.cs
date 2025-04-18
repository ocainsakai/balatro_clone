using Game.Services;
using Game.States;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour, IManager
    {
        private readonly StateMachine stateMachine;
        private readonly AudioService audioService;
        private readonly UIService uiService;


        [Inject]
        public GameManager(StateMachine stateMachine, AudioService audioService, UIService uiService)
        {
            this.stateMachine = stateMachine;
            this.audioService = audioService;
            this.uiService = uiService;
        }

        public void Cleanup()
        {
            //throw new System.NotImplementedException();
        }

        public void Initialize()
        {

            //stateMachine.ChangeState(new MainMenuState());
        }

        public void ResetManager()
        {
            //throw new System.NotImplementedException();
        }
    }

}
