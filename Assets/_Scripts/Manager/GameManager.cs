using System;
using System.Collections;
using UnityEngine;


public class GameManager : SingletonAbstract<GameManager>
{
    PlayingManager playManager => PlayingManager.instance;
    BlindManager blindManager => BlindManager.instance;
    UIManager uiManager => UIManager.instance;
    public Phase currentPhase;
    public class Run {
        public int round_score;
        public int play_hands;
        public int discards;
        public int money;
        public int ante;
        public int round;
    }
    public Run run { get; private set; }
    public event Action<Run> OnRunUpdate;
    public event Action<Poker> OnPokerUpdate;
    public event Action<string> PhaseChanged;
    

    
    void Start()
    {
        InitGame();
    }
    public void InitGame()
    {
        playManager.Initialize();
        blindManager.Initialize();

        run = new Run()
        {
            round_score = 0,
            play_hands = 4,
            discards = 4,
            money = 0,
            ante = 1,
            round = 1
        };
        OnRunUpdate?.Invoke(run);
        OnPokerUpdate?.Invoke(new Poker());

        ChangePhase(Phase.blind);
    }

    public void ChangePhase(Phase newPhase)
    {
        currentPhase = newPhase;
        switch (currentPhase)
        {
            case Phase.blind:
                StartChoosePhase();
                break;
            case Phase.playing:
                StartPlayingPhase();
                break;
            case Phase.shop:
                StartShopPhase();
                break;
        }
    }

    public void StartChoosePhase()
    {
        uiManager.HideAll();
        uiManager.ShowBlind();
        PhaseChanged?.Invoke("Phase: Blind");
    }
    public void StartPlayingPhase()
    {
        uiManager?.HideAll();
        uiManager.ShowAction();
        uiManager.ShowHand();
        uiManager.ShowDeck();
        PhaseChanged?.Invoke("Phase: Play");
        StartCoroutine(playManager.DrawHand());
    }
    public void StartShopPhase()
    {
        
        PhaseChanged?.Invoke("Phase: Shop");

    }
    public void StartGameOverPhase()
    {

    }
    
    //public bool CheckWin()
    //{
    //    if (blindManager.IsWin(run.round_score))
    //    {
    //        Debug.Log("you defeat this blind");
    //        blindManager.Defeat();
    //        EndPlay();
    //        ChangePhase(Phase.blind);
    //        return true;
    //    } else if (run.play_hands <= 0)
    //    {
    //        //Debug.Log("you lose");
    //        ChangePhase(Phase.gameover);
    //        return true;
    //    }
    //    return false;

    //}
    //public void EndPlay()
    //{
    //    OnRunUpdate?.Invoke(run);
    //    OnPokerUpdate?.Invoke(new Poker());

    //    deckManager.End();

    //}
    

    public void SetupRun()
    {
        run.round_score = 0;
        run.play_hands = 4;
        run.discards = 4;
    }

    public void UpdateRunUI()
    {
        OnRunUpdate?.Invoke(run);
    }
    public void UpdatePokerUI(Poker poker)
    {
        OnPokerUpdate?.Invoke(poker);
    }
   
}


public enum Phase
{
    blind,
    playing,
    shop,
    gameover
}