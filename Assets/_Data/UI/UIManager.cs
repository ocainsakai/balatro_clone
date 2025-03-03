
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public MetaUI meta;
    public GameObject leftPanel;
    public GameObject playPhaseUI;
    public GameObject shopPhaseUI;
    public GameObject chooseBossPhaseUI;
    //public GameObject resultPhaseUI;
    
    public void ShowPhaseUI(GamePhase phase)
    {
        playPhaseUI.SetActive(false);
        shopPhaseUI.SetActive(false);
        chooseBossPhaseUI.SetActive(false);

        //resultPhaseUI.SetActive(false);

        switch (phase)
        {
            case GamePhase.Draw:
            case GamePhase.Play:
                playPhaseUI.SetActive(true);
                break;
            case GamePhase.Shop:
                shopPhaseUI.SetActive(true);
                break;
            case GamePhase.NextBlind:
            case GamePhase.BossBlind:
                chooseBossPhaseUI.SetActive(true);
                break;
            //case GamePhase.GameOver:
            //    resultPhaseUI.SetActive(true);
                //break;
        }
    }

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
   
}
