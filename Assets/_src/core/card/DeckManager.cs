using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
namespace Balatro.Core.Card
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private List<CardDataSO> cardPool 
            => Resources.LoadAll<CardDataSO>(typeof(CardDataSO).Name).ToList();
        //[SerializeField] private CardFactory _factory;
        //private List<CardView> _createdCards = new List<CardView>();
        private List<CardDataSO> _deck = new List<CardDataSO>();
        private List<CardDataSO> _drawnPile = new List<CardDataSO>();

        
        // Khởi tạo bộ bài
        public void InitializeDeck()
        {
            //_factory.ClearAllCards();
            //_createdCards.Clear();
            _deck.Clear();
            _drawnPile.Clear();

            foreach (var item in cardPool)
            {
                
                _deck.Add(item);
                //_createdCards.Add(cardView);
            }
            Shuffle();
        }

        // Xáo bài
        public void Shuffle()
        {
            //var combined = _deck.Zip(_createdCards, (runtime, view) => (runtime, view)).ToList();
            System.Random random = new System.Random();
            //combined = combined.OrderBy(x => random.Next()).ToList();

            _deck = _deck.OrderBy(x => random.Next()).ToList();
            //_createdCards = combined.Select(x => x.view).ToList();
        }

        // Rút bài
        public CardDataSO DrawCard()
        {
            if (_deck.Count == 0)
            {
                // Nếu hết bài, đổ lại bài từ discard pile
                RestoreDeckFromDiscardPile();
            }

            if (_deck.Count > 0)
            {
                CardDataSO drawnCard = _deck[0];
                //CardView cardView = _createdCards[0];
                _drawnPile.Add(drawnCard);
                _deck.RemoveAt(0);
                //_createdCards.RemoveAt(0);
                return drawnCard;
            }

            throw new InvalidOperationException("No cards left in the deck.");
        }
        public List<CardDataSO> DrawCards(int count)
        {
            List<CardDataSO> drawnCards = new List<CardDataSO>();
            for (int i = 0; i < count; i++)
            {
                drawnCards.Add(DrawCard());
            }
            return drawnCards;
        }

        // Bỏ bài vào discard pile
        public void DiscardCard(CardDataSO card)
        {
            _drawnPile.Add(card);
        }

        // Khôi phục bộ bài từ discard pile
        private void RestoreDeckFromDiscardPile()
        {
            _deck.AddRange(_drawnPile);
            _drawnPile.Clear();
            Shuffle();
        }
        public int RemainingCardCount => _deck.Count;
    }
}
