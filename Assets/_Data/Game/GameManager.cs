using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private DeckManager deckManager;
    private UIManager uiManager => UIManager.Instance;
    private LevelManager levelManager;

    
    private int targetScore;
    private int currentScore;
    private int playCount;
    private int discardCount;
    private int currentLevel;


    public GamePhase CurrentPhase { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        deckManager = GetComponent<DeckManager>();
        levelManager = GetComponent<LevelManager>();
        
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
        uiManager.ShowPhaseUI(GamePhase.Play);
        deckManager.DrawCards();
        ChangePhase(GamePhase.Play);
    }
    void StartPlayPhase()
    {
        // bat tat ui
    }
    void StartNextBlindPhase()
    {
        uiManager.ShowPhaseUI(GamePhase.NextBlind);
        //currentLevel++;
        //NewGame(levelManager.GetLevel(currentLevel));
        //ChangePhase(GamePhase.Draw);

    }
    public void PlayHand()
    {
        if (playCount <= 0) return;
        currentScore += deckManager.Calculate();
        playCount--;

        uiManager.meta.UpdateText("score", currentScore);
        uiManager.meta.UpdateText("play", playCount);

        ChangePhase(GamePhase.Scoring);
    }
    
    public void Discard()
    {
        if (discardCount <= 0) return;
        deckManager.Discard();
        discardCount--;
        uiManager.meta.UpdateText("discard", discardCount);

    }
    public void StartBossBlindPhase()
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
        uiManager.ShowPhaseUI(GamePhase.Shop);
        deckManager.HideHand();
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
        uiManager.ShowPhaseUI(GamePhase.Draw);
        uiManager.meta.UpdateText("score", 0);
        uiManager.meta.UpdateText("play", playCount);
        uiManager.meta.UpdateText("discard", discardCount);
        uiManager.meta.UpdateText("predict", targetScore);

        deckManager.ShuffleDeck();
        deckManager.DrawCards();
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
