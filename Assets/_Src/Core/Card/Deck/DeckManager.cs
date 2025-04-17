
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Balatro.Cards.System
{

    public class DeckManager : BaseManager
    {
        [SerializeField] CardDatabase cardDatabase;
        [SerializeField] DeckView deckView;


        private List<CardData> defaultDeck = new List<CardData>();
        public List<CardData> GetDefaultDeck() => defaultDeck;

        private List<CardData> playingDeck = new List<CardData>();
        public List<CardData> GetPlayingDeck() => playingDeck;

        private List<CardData> discardPile = new List<CardData>();
        public bool isEmpty => playingDeck.Count == 0;
        public bool shuffeLock = true;
        public event Action OnDeckChanged;
        public override void Initialize()
        {
            defaultDeck.AddRange(cardDatabase.database);
            playingDeck.AddRange(defaultDeck);
            if (!shuffeLock) Shuffe();
        }
        public CardData Draw()
        {
            if (isEmpty)
            {
                playingDeck.AddRange(discardPile);
                discardPile.Clear();
                if (!shuffeLock) Shuffe();

            }
            CardData card = playingDeck[0];
            playingDeck.Remove(card);
            OnDeckChanged?.Invoke();
            return card;
        }
        public void Shuffe()
        {
            playingDeck = playingDeck.OrderBy(c => UnityEngine.Random.value).ToList();
        }
        public void ClearDeck()
        {
            discardPile.Clear();
            playingDeck?.Clear();
        }
    }
}

