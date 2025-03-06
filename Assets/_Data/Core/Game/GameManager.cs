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


    public RunPhase CurrentPhase { get; private set; }
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
        NewRun();
        //NewGame(levelManager.GetLevel(currentLevel));
    }
    public void ChangePhase(RunPhase newPhase)
    {
        CurrentPhase = newPhase;
        Debug.Log("Chuyển sang Phase: " + newPhase);
        switch (newPhase)
        {
            case RunPhase.Blind:
                StartBlindPhase();
                break;
            case RunPhase.Scoring:
                StartScoringPhase();
                break;
            case RunPhase.Shoping:
                break;
            
        }
    }
   
    private void StartBlindPhase()
    {
        uiManager.ShowPhaseUI(RunPhase.Blind);
    }
    public void PlayHand()
    {
        if (playCount <= 0) return;
        currentScore += deckManager.Calculate();
        playCount--;

        uiManager.meta.UpdateText("score", currentScore);
        uiManager.meta.UpdateText("play", playCount);

        ChangePhase(RunPhase.Scoring);
    }
    
    public void Discard()
    {
        if (discardCount <= 0) return;
        deckManager.Discard();
        discardCount--;
        uiManager.meta.UpdateText("discard", discardCount);

    }


    private void StartScoringPhase()
    {

        deckManager.ShuffleDeck();
        deckManager.DrawCards();

        if (currentScore >= targetScore)
        {
            //ChangePhase(RunPhase.Shop);
        }
        else if (playCount == 0)
        {
            //ChangePhase(RunPhase.GameOver);
        }
    }
 
    public void NewRun()
    {
        playCount = 4;
        discardCount = 4;
        currentScore = 0;

        levelManager.NewRun();

        ChangePhase(RunPhase.Blind);

        uiManager.meta.UpdateText("score", 0);
        uiManager.meta.UpdateText("play", playCount);
        uiManager.meta.UpdateText("discard", discardCount);
        uiManager.meta.UpdateText("predict", targetScore);

    }
}

public enum RunPhase
{
    Blind,
    Scoring,
    Shoping,
}
