using Card;
using Poker;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] DeckManager deckManager;
        [SerializeField] CardManager cardContainer;

        [SerializeField] IntVariable handSize;
    
        [SerializeField] StandardCardEvent onCardPlayed;
        [SerializeField] PokerHandRuntime currentPokerHand;
        

        void Start()
        {
            deckManager.InitializeDeck();
            //currentPokerHand.OnPokerChanged += UpdatePoker;
            DrawCards(); 
        }

        public void PlayHand()
        {
            
            
        }

        void DefeatBlind()
        {
            
             
        }

        public void DrawCards()
        {
            var cardDrawn = deckManager.DrawCards(handSize.Value - cardContainer.HandCount);
        }
    }
}

