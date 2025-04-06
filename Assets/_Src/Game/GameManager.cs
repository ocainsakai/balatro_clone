using Blind;
using Card;
using Poker;
using System;
using UnityEngine;



namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static long BlindScore = 0;
        [SerializeField] CardManager cardManager;
        [SerializeField] BlindController blindController;
        [SerializeField] PokerHandRuntime currentPokerHand;
        [SerializeField] IntVariable roundScore;
        public GameStateEvent changeGameState;
        private GameState _gameState;
        public GameState State
        {
            get => _gameState;
            set
            {
                if (value == _gameState) return;
                _gameState = value;
                switch (value)
                {
                    case GameState.Blinding:
                        HandleBlinding();
                        break;
                    case GameState.Playing:
                        HandlePlaying();
                        break;
                    case GameState.Decide:
                        HandleDecide();
                        break;
                    case GameState.Shopping:
                        HandleShopping();
                        break;
                }
            }
        }

        private void HandleDecide()
        {

            if (roundScore.Value >= blindController.BlindScore)
            {
                cardManager.ClearHand();
                blindController.Defeat();
                roundScore.Value = 0;
                changeGameState.Raise(GameState.Blinding);
            }
            else
            {
                cardManager.RemoveSelectedCards();
                changeGameState.Raise(GameState.Playing);
            }

        }

        private void HandleShopping()
        {
        }

        private void HandlePlaying()
        {
            cardManager.Draw();

        }
        private void HandleBlinding()
        {
            
        }

        private void Start()
        {
            changeGameState.Raise(GameState.Blinding);
        }
        

       
    }
    public enum GameState
    {
        None,
        Blinding,
        Playing,
        Decide,
        Shopping,
    }
}