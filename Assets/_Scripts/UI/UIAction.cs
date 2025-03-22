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
        playHand.onClick.AddListener(() => DeckManager.instance.PlayHand());
        sortByRank.onClick.AddListener(() => DeckManager.instance.Sort(1));
        sortBySuit.onClick.AddListener(() => DeckManager.instance.Sort(2));
        discard.onClick.AddListener(() => DeckManager.instance.Discard());
    }
    private void OnDisable()
    {
        //playHand.onClick.RemoveAllListeners();
        //discard.onClick.RemoveAllListeners();
    }
}
