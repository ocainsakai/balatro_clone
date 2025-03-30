using Balatro.Blind;
using Balatro.Cards;
using Balatro.UI;
using UnityEngine;

namespace Balatro.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] DeckManager deckManager;
        [SerializeField] BlindManager blindManager;
        [SerializeField] PhaseManager phaseManager;
        [SerializeField] UIManager uiManager;
        [SerializeField] UIPhaseInfo phaseInfo;


        public int blindScore => blindManager.blindScore;
        public int totalScore;
        public int pokerChip;
        public int pokerMul;
        public int handCount; 
        public int discardCount; 
        public int money; 
        public int round;
        public int ante => blindManager._anteLevel;

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

            deckManager.AddChip += AddChip;
            deckManager.AddMul += AddMul;
            deckManager.OnScoreChange += CheckWin;
            deckManager.OnPokerChange += UpdatePokerHand;
            deckManager.NextRound += OnNextRound;

            blindManager.NextAnte += OnNextAnte;
            blindManager.OnSelectBlind += OnSelectBlind;

            phaseManager.StartSelectPhase += StartSelectPhase;
            phaseManager.StartPlaying += StartPlayPhase;
            
        }

        private void OnNextAnte()
        {
            uiManager.uiRun.UpdateAnte(ante);
            //throw new System.NotImplementedException();
        }

        private void OnNextRound()
        {
            round++;
            uiManager.uiRun.UpdateRoundIndex(round);
            //throw new System.NotImplementedException();
        }

        public void Start()
        {
            NewGame();
        }
        private void UpdatePokerHand(PokerHandType handType)
        {
            pokerChip = handType.BaseChips;
            pokerMul = handType.BaseMultiple;
            uiManager.uiRun.UpdatePokerName(handType.Name);
            uiManager.uiRun.UpdatePokerChip(handType.BaseChips);
            uiManager.uiRun.UpdatePokerMul(handType.BaseMultiple);
        }
        private void AddMul(int obj)
        {
            pokerMul += obj;
            uiManager.uiRun.UpdatePokerMul(pokerMul);
        }

        private void AddChip(int obj)
        {
            pokerChip += obj;
            uiManager.uiRun.UpdatePokerChip(pokerChip);
        }

        public void NewGame()
        {
            round = 1;
            deckManager.ResetDeck();
            phaseManager.ChangePhase(Phase.SelectPhase);
        }
        public void StartSelectPhase()
        {
            uiManager.ShowBlindUI();
            phaseInfo.UpdatePhase(Phase.SelectPhase);
            blindManager.StartPhase();
            UpdateUI();
        }

        private void OnSelectBlind()
        {
            phaseManager.ChangePhase(Phase.PlayPhase);
        }

        public void StartPlayPhase()
        {
            uiManager.ShowPlayUI();
            phaseInfo.ShowBlind(blindManager.currentBlind,blindManager.blindScore);
            deckManager.StartPhase();
            NewPlayTurn();
        }
        private void NewPlayTurn()
        {
            //this.blindScore = blindScore;
            handCount = 4;
            discardCount = 4;
            totalScore = 0;
            UpdateUI();
        }
        private void UpdateUI()
        {
            uiManager.uiRun.UpdateAnte(ante);
            uiManager.uiRun.UpdateDiscardCount(discardCount);
            uiManager.uiRun.UpdateHandCount(handCount);
            uiManager.uiRun.UpdateMoney(money);
            uiManager.uiRun.UpdateRoundIndex(round);
            uiManager.uiRun.UpdateRoundScore(totalScore);
            UpdatePokerHand(PokerHandType.None);
        }

        public void CheckWin()
        {

            totalScore += pokerChip * pokerMul;
            uiManager.uiRun.UpdateRoundScore(totalScore);

            if (totalScore >= blindScore)
            {
                deckManager.ResetDeck();
                blindManager.Defeat();
                StartSelectPhase();
            } else if (handCount <=0)
            {
                // game over
            } else
            {
                deckManager.DiscardAndDraw();
                Debug.Log("discard");
            }
        }
        //public void DrawHand()
        //{
        //    for (int i = handManager.hand_count; i < handManager.MAX_HAND_SIZE; i++)
        //    {
        //        CardDataSO card = deckManager.DrawCard();
        //        handManager.AddCardToHand(card);
        //    }
        //    handManager.Sort();
        //}
        //public void Discard()
        //{
        //    StartCoroutine(DiscardProcess());
        //}
        //private IEnumerator DiscardProcess()
        //{
        //    yield return handManager.RemoveSelected();
        //    DrawHand();

        //}
        //public void PlayHand()
        //{
        //    StartCoroutine(PlayHandProcess());
        //}
        //private IEnumerator PlayHandProcess()
        //{
        //    yield return StartCoroutine(handManager.CalculateSelected());
        //    DrawHand();
        //}
    }
    public enum Phase
    {
        SelectPhase,
        PlayPhase,
        ShopPhase,
        Gameover,
    }
}
