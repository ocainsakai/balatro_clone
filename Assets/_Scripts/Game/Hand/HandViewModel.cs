using Game.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using static Game.Cards.CardsSorter;

namespace Game.Player.Hands
{
    public class HandViewModel
    {
        private ReactiveCollection<Card> _cards = new ReactiveCollection<Card>();
        public IReadOnlyReactiveCollection<Card> Cards => _cards;

        public Subject<Card> OnCardStateChanged = new Subject<Card>();
        private ReactiveProperty<bool> CanSelectCard = new ReactiveProperty<bool>();
        public int Count => _cards.Count;

        public HandViewModel()
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
        public void Add(Card card)
        {
            _cards.Add(card);
            card.State.Subscribe(x => OnCardStateChanged.OnNext(card));
        }
        public void Remove(Card card)
        {
            _cards.Remove(card);
        }
        public void ResetAllCards()
        {
            foreach (var card in Cards)
            {
                card.State.Value = CardState.OnHand;
                card.CanSelect = true;
            }
        }
        public IEnumerator Sort()
        {
            Sort(CardsSorter.currentType);
            yield return null;
        }
        public void Sort(SortType sortType)
        {
            if (Cards.Count == 0) return;
            ResetAllCards();
            var sortedCards = (sortType == SortType.ByRank) ? CardsSorter.SortByRank(Cards) : CardsSorter.SortBySuit(Cards);
            for (int i = 0; i < sortedCards.Count(); i++)
            {
                var card = sortedCards.ElementAt(i);
                int currentIndex = _cards.IndexOf(card);
                if (currentIndex != i)
                {
                    _cards.Move(currentIndex, i);
                }
            }
        }
    }
}