using Card;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Core
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] List<StandardCardSO> deck;
        [SerializeField] StandardCardEvent onCardDraw;
        [SerializeField] StandardCardEvent onDiscardCard;
        public List<IStandardCard> discardPile = new List<IStandardCard>();
        public List<IStandardCard> playingdeck;
        public void Start()
        {
            playingdeck = new List<IStandardCard>(deck.Select(x => x.data));

        }
        public void InitializeDeck()
        {
            ShuffleDeck();
        }

        public void ShuffleDeck()
        {
            for (int i = playingdeck.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (playingdeck[i], playingdeck[randomIndex]) = (playingdeck[randomIndex], playingdeck[i]);
            }
        }
        public void AddToPile(IStandardCard card)
        {
            discardPile.Add(card);
        }
        public IStandardCard DrawCard()
        {
            if (playingdeck.Count <= 0)
            {
                RestoreFormDiscardPile();
            }
            IStandardCard card = playingdeck[0];
            playingdeck.RemoveAt(0);
            onCardDraw.Raise(card);
            return card;
        }
        public List<IStandardCard> DrawCards(int amount)
        {
            List<IStandardCard> drawnCards = new List<IStandardCard>();
            for (int i = 0; i < amount; i++)
            {
                drawnCards.Add(DrawCard());
            }
            return drawnCards;
        }
        private void RestoreFormDiscardPile()
        {
            playingdeck.AddRange(discardPile);
            discardPile.Clear();
        }
    }
}

