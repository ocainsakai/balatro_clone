
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public MetaUI meta;
    public GameObject leftPanel;
    public GameObject playPhaseUI;
    public GameObject shopPhaseUI;
    public GameObject blindPhaseUI;
    //public GameObject resultPhaseUI;
    
    public void ShowPhaseUI(RunPhase phase)
    {
        playPhaseUI.SetActive(false);
        shopPhaseUI.SetActive(false);
        blindPhaseUI.SetActive(false);

        //resultPhaseUI.SetActive(false);
        switch (phase)
        {
            case RunPhase.Blind:
                blindPhaseUI.SetActive(true);
                break;
            case RunPhase.Scoring:
                playPhaseUI.SetActive(true);
                break;
            case RunPhase.Shoping:
                shopPhaseUI.SetActive(true);
                break;
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
