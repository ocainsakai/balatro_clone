using Game.Managers;
using System;
using UniRx;
using Zenject;

namespace Game.CardSystem
{
    public class CardManager : IManager
    {
        private readonly CardDatabase cardDatabase;
        private readonly CardFactory cardFactory;
        //private readonly AddressableCardLoader loader;
        private readonly Subject<ICard> onCardCreated = new Subject<ICard>();

        [Inject]
        public CardManager(CardDatabase cardDatabase, CardFactory cardFactory)
        {
            this.cardDatabase = cardDatabase;
            this.cardFactory = cardFactory;
            //this.loader = loader;
        }

        public IObservable<ICard> OnCardCreated => onCardCreated;

        public void CreateCard(string cardId, Action<ICard> onCreated)
        {
            //cardDatabase.LoadCard(cardId, cardData =>
            //{
            //    if (cardData == null)
            //    {
            //        Debug.LogError($"Card with ID {cardId} not found!");
            //        onCreated?.Invoke(null);
            //        return;
            //    }

            //    var card = cardFactory.Create(cardData);
            //    onCardCreated.OnNext(card);
            //    onCreated?.Invoke(card);
            //});
        }

        public void CreateCardView(ICard card, Action<CardView> onCreated)
        {
            //loader.LoadCardView();
            //loader.OnCardViewLoaded
            //    .First()
            //    .Subscribe(cardView =>
            //    {
            //        cardView.Construct(card, Container.Resolve<UIService>());
            //        onCreated?.Invoke(cardView);
            //    });
        }
        public void Cleanup()
        {
        }

        public void Initialize()
        {
        }

        public void ResetManager()
        {
        }
    }

}
