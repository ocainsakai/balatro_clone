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


    private void Awake()
    {
        deckManager = GetComponent<DeckManager>();
        uiManager = GetComponent<UIManager>();
        levelManager = GetComponent<LevelManager>();
    }

    public void PlayHand()
    {
        if (playCount <= 0) return;
        currentScore += deckManager.Calculate();
        playCount--;

        uiManager.UpdateText("score", currentScore);
        uiManager.UpdateText("play", playCount);

    }

    public void Discard()
    {
        if (discardCount <= 0) return;
        deckManager.Discard();
        discardCount--;
        uiManager.UpdateText("discard", discardCount);

    }
    void Start()
    {
        NewGame(levelManager.GetLevel(0));
    }

    private void CheckScore()
    {
        if (currentScore >= targetScore)
        {
            // winn
        }
        else if (playCount == 0)
        {
            //lose
        }
    }
    // add level in para
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
    }
}
