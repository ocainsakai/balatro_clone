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
        playHand.onClick.AddListener(() => PlayingManager.instance.PlayHand());
        sortByRank.onClick.AddListener(() => PlayingManager.instance.Sort(1));
        sortBySuit.onClick.AddListener(() => PlayingManager.instance.Sort(2));
        discard.onClick.AddListener(() => PlayingManager.instance.Discard());
    }
    private void OnDisable()
    {
        //playHand.onClick.RemoveAllListeners();
        //discard.onClick.RemoveAllListeners();
    }
}
