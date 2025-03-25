using UnityEngine;
using UnityEngine.UI;


public class UIPlayButtons : MonoBehaviour
{
    [SerializeField] private Button playHand;
    [SerializeField] private Button sortByRank;
    [SerializeField] private Button sortBySuit;
    [SerializeField] private Button discard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(running);
        playHand.onClick.AddListener(() => UIHand.instance.PlayHand());
        sortByRank.onClick.AddListener(() => UIHand.instance.SortByRank());
        sortBySuit.onClick.AddListener(() => UIHand.instance.SortBySuit());
        discard.onClick.AddListener(() => UIHand.instance.Discard());
    }
}
