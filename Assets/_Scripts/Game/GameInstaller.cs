
using Game.Pokers;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<PokerViewModel>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<DeckManager>();
            builder.RegisterComponentInHierarchy<ScoreManager>();
            builder.RegisterComponentInHierarchy<HandManager>();
        }
    }
}