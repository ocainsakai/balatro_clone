using Balatro.Blind;
using Balatro.Cards;
using Balatro.UI;
using System.Collections;
using UnityEngine;

namespace Balatro.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [SerializeField] DeckManager deckManager;
        [SerializeField] HandManager handManager;
        [SerializeField] UIManager uiManager;
        [SerializeField] UIPhaseInfo phaseInfo;

        public int blindScore;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public void Start()
        {
            NewGame();
        }
        public void NewGame()
        {
            StartSelectPhase();
        }
        public void StartSelectPhase()
        {
            uiManager.ShowBlindUI();
            phaseInfo.UpdatePhase(Phase.SelectPhase);
        }
        public void StartPlayPhase(BlindDataSO blindData, int blindScore)
        {
            uiManager.ShowPlayUI();
            phaseInfo.ShowBlind(blindData, blindScore); 
            this.blindScore = blindScore;
            deckManager.InitializeDeck();

            DrawHand();
        }
        public void DrawHand()
        {
            for (int i = handManager.hand_count; i < handManager.MAX_HAND_SIZE; i++)
            {
                CardDataSO card = deckManager.DrawCard();
                handManager.AddCardToHand(card);
            }
            handManager.Sort();
        }
        public void Discard()
        {
            StartCoroutine(DiscardProcess());
        }
        private IEnumerator DiscardProcess()
        {
            yield return handManager.RemoveSelected();
            DrawHand();

        }
        public void PlayHand()
        {
            StartCoroutine(PlayHandProcess());
        }
        private IEnumerator PlayHandProcess()
        {
            yield return StartCoroutine(handManager.CalculateSelected());
            DrawHand();
        }
    }
    public enum Phase
    {
        SelectPhase,
        PlayPhase,
        ShopPhase,
    }
}
