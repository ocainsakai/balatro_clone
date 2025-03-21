using TMPro;
using UnityEngine;

public class UIPokerHand : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI poker_name;
    [SerializeField] private TextMeshProUGUI chips;
    [SerializeField] private TextMeshProUGUI multiple;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.OnPokerUpdate += UpdatePokerHand;
    }

    // Update is called once per frame
    void UpdatePokerHand(Poker poker)
    {
        poker_name.text = poker.name;  
        chips.text = poker.chips + "";
        multiple.text = poker.multiple + "";
    }
}
