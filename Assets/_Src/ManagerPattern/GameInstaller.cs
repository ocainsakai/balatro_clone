using Game.Managers;
using Game.Services;
using Game.States;
using Zenject;

namespace Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.Bind<StateMachine>().AsSingle();
            Container.Bind<AudioService>().AsSingle();
            Container.Bind<UIService>().AsSingle();
            Container.Bind<InputService>().AsSingle();
            Container.Bind<SaveLoadService>().AsSingle();
        }
    }
}

