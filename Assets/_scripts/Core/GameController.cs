using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Run running;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void NewRun()
    {
        DeckSO deck = DeckManager.Instance.GetCurrentDeck();
        if (deck.IsLocked) return;
        running = new Run(StakeDifficult.Low, deck);
        SceneManager.LoadScene("PlayRun");
    }
    public void ContinueRun()
    {

    }
}
