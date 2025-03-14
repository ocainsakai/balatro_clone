using UnityEngine;
using UnityEngine.UI;

public class PlayButtons : MonoBehaviour
{
    Run running => GameManager.Instance.running;
    [SerializeField] private Button playHand;
    [SerializeField] private Button sortByRank;
    [SerializeField] private Button sortBySuit;
    [SerializeField] private Button discard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        //Debug.Log(running);
        playHand.onClick.AddListener(() => running.PlayHand());
        discard.onClick.AddListener(() => running.Discard());
    }
    private void OnDisable()
    {
        playHand.onClick.RemoveAllListeners();
        discard.onClick.RemoveAllListeners();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
