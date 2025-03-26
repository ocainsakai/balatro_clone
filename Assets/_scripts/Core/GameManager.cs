using System.Collections;
using UnityEngine;


public class GameManager : SingletonAbs<GameManager>
{
    [SerializeField] UIManager uiManager;
    [SerializeField] BlindManager blindManager;
    [SerializeField] UIHandManager handManager;
    [SerializeField] UIRunManager uiRun;
    [SerializeField] UIPlayButtons uiPlayBtn;
    //[SerializeField] DeckManager deckManager;
 
    //[SerializeField] UIPokerHand ui_poker_hand;

    [SerializeField] GameObject deck;
    public class Run
    {
        public int round_score;
        public int play_hands;
        public int discards;
        public int money;
        public int ante;
        public int round;
    }
    public Run run { get; private set; }

    public Phase currentPhase { get; private set; }

    void Start()
    {
        InitGame();
    }
    public void ChangePhase(Phase phase)
    {
        currentPhase = phase;
        switch (currentPhase)
        {
            case Phase.Blind:
                StartBlindPhase();
                break;
            case Phase.Score:
                StartPlayingPhase();
                break;
            case Phase.Shop: 
                break;
        }
    }
    public void StartBlindPhase()
    {
        HideAll();
        blindManager.gameObject.SetActive(true);
        blindManager.Initlize();
    }
    public void StartPlayingPhase()
    {
        HideAll();
        deck.gameObject.SetActive(true);
        uiPlayBtn.gameObject.SetActive(true);
        handManager.gameObject.SetActive(true);
        //handManager.Initlize(deckManager.defaulsCards);
    }
    public void InitGame()
    {
        run = new Run()
        {
            round_score = 0,
            play_hands = 4,
            discards = 4,
            money = 0,
            ante = 1,
            round = 1
        };
        uiManager.NonePoker();
        uiRun.UpdateRunUI(run);
        ChangePhase(Phase.Blind);
    }
    public void PlayHand()
    {
        run.play_hands--;
        run.round++;
        uiRun.UpdateRunUI(run);
    }
    public void Discard()
    {
        run.discards--;
        uiRun.UpdateRunUI(run);
    }
    public void HideAll()
    {
        blindManager.gameObject.SetActive(false);
        uiPlayBtn.gameObject.SetActive(false);
        //uiRun.gameObject.SetActive(false);
        handManager.gameObject.SetActive(false);
        deck.gameObject.SetActive(false);
    }
    //public IEnumerator AddScore(int score)
    //{
    //    yield return (uiRun.AddScore(score));
    //    run.round_score += score;
    //}
}
public enum Phase
{
    Blind = 1,
    Score = 2,
    Shop = 3
}