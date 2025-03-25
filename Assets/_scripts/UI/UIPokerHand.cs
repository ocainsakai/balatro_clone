
using TMPro;
using UnityEngine;
using DG.Tweening;



public class UIPokerHand : UITextAbstract
{
    [SerializeField] private TextMeshProUGUI poker_name;
    [SerializeField] private TextMeshProUGUI chips;
    [SerializeField] private TextMeshProUGUI multiple;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    public void UpdatePokerHand(Poker poker)
    {
        JumpText(poker_name, poker.name, 0.3f);
        JumpText(chips, poker.chips.ToString(), 0.3f);
        JumpText(multiple, poker.multiple.ToString(), 0.3f);

    }
    public void UpdateChip(int chips_amount, float duration)
    {
        int currentChip = int.Parse(chips.text);
        int target = currentChip + chips_amount;
        JumpText(chips, target.ToString(), duration);
    }

}