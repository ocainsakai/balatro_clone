using Game.Cards;
using Game.Cards.Decks;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using VContainer;
using static Game.Cards.CardsSorter;

namespace Game.Player.Hands
{
    public class HandViewModel : CardCollection
    {
        public Subject<Card> OnCardStateChanged = new Subject<Card>();
        private ReactiveProperty<bool> CanSelectCard = new ReactiveProperty<bool>();
        private Deck deck;
        [Inject] 
        public HandViewModel(PlayManager playManager, Deck deck)
        {
            this.deck = deck;
            InitSelectCondition();
            playManager.OnDraw.Subscribe(x => DrawHand());
            playManager.OnDiscard.Subscribe();
        }
        public void DrawHand()
        {
            deck.Shuffle();
            int amount = 8 - Count;
            for (int i = 0; i < amount; i++)
            {
                var card = deck.GetFirst();
                card.State.Subscribe(x => OnCardStateChanged.OnNext(card));
                Add(card);
            }
            ResetAllCards();   
        }
        private void InitSelectCondition()
        {
            OnCardStateChanged.Subscribe(x =>
            {
                CanSelectCard.Value = GetCardInState(CardState.Selected).Count() < 5;
            });
            CanSelectCard.Subscribe(x => SetCanSelectCards(CanSelectCard.Value));
        }

        private void SetCanSelectCards(bool canSelect)
        {
            foreach (var card in _cards)
            {
                card.CanSelect = canSelect;
            }
        }
        public IEnumerable<Card> GetCardInState(CardState state)
        {
            return _cards.Where(x => x.State.Value == state);
        }

        public void ResetAllCards()
        {
            foreach (var card in Cards)
            {
                card.State.Value = CardState.Hold;
            }
            SetCanSelectCards(true);
        }
        public void RemoveCardInState(CardState cardState)
        {
            for (int i = _cards.Count - 1; i >= 0; i--)
            {
                var card = _cards[i];
                if (card.State.Value == cardState) _cards.RemoveAt(i);
            }
        }
    }
}