using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DeckManager deckManager;
    private UIManager uiManager;
    private LevelManager levelManager;


    private int targetScore;
    private int currentScore;
    private int playCount;
    private int discardCount;
    private int currentLevel;


    public GamePhase CurrentPhase { get; private set; }
    private void Awake()
    {
        deckManager = GetComponent<DeckManager>();
        uiManager = GetComponent<UIManager>();
        levelManager = GetComponent<LevelManager>();
    }

    private void Start()
    {
        currentLevel = 0;
        NewGame(levelManager.GetLevel(currentLevel));
    }
    public void ChangePhase(GamePhase newPhase)
    {
        CurrentPhase = newPhase;
        Debug.Log("Chuyển sang Phase: " + newPhase);

        switch (newPhase)
        {
            case GamePhase.Draw:
                StartDrawPhase();
                break;
            case GamePhase.Play:
                StartPlayPhase();
                break;
            case GamePhase.Scoring:
                StartScoringPhase();
                break;
            case GamePhase.Shop:
                StartShopPhase();
                break;
            case GamePhase.NextBlind:
                StartNextBlindPhase();
                break;
            case GamePhase.BossBlind:
                StartBossBlindPhase();
                break;
            case GamePhase.GameOver:
                StartGameOver();
                break;
        }
    }
    void StartDrawPhase()
    {
        // Rút bài từ Deck
        deckManager.DrawCards();
        ChangePhase(GamePhase.Play);
    }
    void StartPlayPhase()
    {
        // bat tat ui
    }
    void StartNextBlindPhase()
    {
        currentLevel++;
        NewGame(levelManager.GetLevel(currentLevel));
        ChangePhase(GamePhase.Draw);
    }
    public void PlayHand()
    {
        if (playCount <= 0) return;
        currentScore += deckManager.Calculate();
        playCount--;

        uiManager.UpdateText("score", currentScore);
        uiManager.UpdateText("play", playCount);

        ChangePhase(GamePhase.Scoring);
    }
    
    public void Discard()
    {
        if (discardCount <= 0) return;
        deckManager.Discard();
        discardCount--;
        uiManager.UpdateText("discard", discardCount);

    }
    void StartBossBlindPhase()
    {
        // Đối đầu Boss
        //BossManager.Instance.StartBossFight();
    }

    private void StartScoringPhase()
    {
        if (currentScore >= targetScore)
        {
            ChangePhase(GamePhase.Shop);
        }
        else if (playCount == 0)
        {
            ChangePhase(GamePhase.GameOver);
        }
    }
    void StartShopPhase()
    {
        // Mở Shop UI để nâng cấp bài
        //UIManager.Instance.ShowShopUI();
    }
    void StartGameOver()
    {
        // return home scene
        //UIManager.Instance.ShowGameOverScreen();
    }
    public void NewGame(LevelSO level)
    {
        playCount = 4;
        discardCount = 4;
        targetScore = level.targetScore;
        currentScore = 0;

        uiManager.UpdateText("score", 0);
        uiManager.UpdateText("play", playCount);
        uiManager.UpdateText("discard", discardCount);
        uiManager.UpdateText("predict", targetScore);

        deckManager.ShuffleDeck();
        deckManager.DrawCards();
        ChangePhase(GamePhase.Draw);
    }
}

public enum GamePhase
{
    Draw,
    Play,
    Scoring,
    Shop,
    NextBlind,
    BossBlind,
    GameOver
}
