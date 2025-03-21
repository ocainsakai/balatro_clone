using UnityEngine;
using UnityEngine.UI;

public class UIAction : MonoBehaviour
{
    [SerializeField] private Button playHand;
    [SerializeField] private Button sortByRank;
    [SerializeField] private Button sortBySuit;
    [SerializeField] private Button discard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(running);
        playHand.onClick.AddListener(() => GameManager.instance.PlayHand());
        sortByRank.onClick.AddListener(() => GameManager.instance.SortByRank());
        sortBySuit.onClick.AddListener(() => GameManager.instance.SortBySuit());
        discard.onClick.AddListener(() => GameManager.instance.Discard());
    }
    private void OnDisable()
    {
        //playHand.onClick.RemoveAllListeners();
        //discard.onClick.RemoveAllListeners();
    }
}
