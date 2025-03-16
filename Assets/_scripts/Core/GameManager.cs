using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Manager Instance")]
    public UIManager uiManager => UIManager.Instance;
    public DeckManager deckManager => DeckManager.Instance;

    [Header("Running Infomation")]
    public RunManager runManager;

    public Phase currentPhase { get; private set; }

    private void Awake()
    {
        Debug.Log("Awake method called");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Set as singleton instance");
        }
        else
        {
            Debug.Log("Instance already exists, destroying this one");
            Destroy(gameObject);
            return;
        }


    }
    private void Start()
    {
#if UNITY_EDITOR
        Debug.Log("In Unity Editor");
        Debug.Log("Current scene: " + SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "PlayRun")
        {
            Debug.Log("In PlayRun scene");

            if (deckManager != null)
            {
                Debug.Log("Deck is valid");
                runManager = new RunManager(StakeDifficult.white, deckManager.GetCurrentDeck());
                ChangePhase(Phase.Blind);

                Debug.Log("Initialized test run in PlayRun scene");
            }
            else
            {
                Debug.LogError("Deck is null!");
            }
        }
#endif
    }
    public void StartNewRun()
    {
        Debug.Log("playyy");
        if (deckManager.SetCurrentDeck())
        {
            runManager = new RunManager(StakeDifficult.white, deckManager.GetCurrentDeck());
            //Debug.Log(running.playingDeck.Count);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("PlayRun");
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayRun")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            ChangePhase(Phase.Blind);
        }
    }
   
    public void ContinueRun()
    {

    }

    public void ChangePhase(Phase newPhase)
    {
        //Debug.Log(running.playingDeck.Count);
        currentPhase = newPhase;
        uiManager.UpdatePhaseUI(currentPhase);

        switch (currentPhase)
        {
            case Phase.Blind:
                StartBlindPhase();
                break;
            case Phase.Score:
                StartScorePhase();
                break;
            case Phase.Shop:
                StartShopPhase();
                break;
        }
    }

    private void StartBlindPhase()
    {

    }
    private void StartScorePhase()
    {
        
        runManager.Draws();
        UIManager.Instance.runUI.UpdateRun(runManager.run);
    }
    private void StartShopPhase()
    {

    }
    
}
public enum Phase
{
    Blind = 1,
    Score = 2,
    Shop = 3
}