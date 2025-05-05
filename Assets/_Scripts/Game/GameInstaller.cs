using Game.Cards;
using Game.Cards.Decks;
using Game.Jokers;
using Game.Player.Hands;
using Game.Pokers;
using Game.System.Score;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterComponentInHierarchy<HandView>();
            builder.RegisterComponentInHierarchy<GameManager>();
            builder.RegisterComponentInHierarchy<CardFactory>();
            builder.Register<Deck>(Lifetime.Singleton);
            builder.Register<HandViewModel>(Lifetime.Singleton);
            builder.Register<PokerViewModel>(Lifetime.Singleton);
            builder.Register<ScoreManager>(Lifetime.Singleton);
            builder.Register<JokerViewModel>(Lifetime.Singleton);
        }
    }
}