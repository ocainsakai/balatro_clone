using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BlindManager : MonoBehaviour
{
    public static BlindManager Instance;

    //private int baseChips = GameManager.Instance.baseChips;
    
    [SerializeField] private List<BlindSO> allBlinds;


    private BlindSO[] currentBlinds = new BlindSO[3];
    private BlindSO smallBlind => allBlinds.FirstOrDefault(t => t.blindName == "Small Blind");
    private BlindSO bigBlind => allBlinds.FirstOrDefault(t => t.blindName == "Big Blind");
    private BlindSO hook => allBlinds.FirstOrDefault(t => t.blindName == "The Hook");

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void Start()
    {
        NewBLinds(1);

    }
    public void NewBLinds(int anteLevel)
    {
        smallBlind.isLocked = false;
        bigBlind.isLocked = true;
        hook.isLocked = true;
        currentBlinds[0] = smallBlind;
        currentBlinds[1] = bigBlind;
        currentBlinds[2] = hook;
        UIManager.Instance.UpdateBlindUI(currentBlinds);

    }
    public void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
