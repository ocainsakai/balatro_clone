using TMPro;
using UnityEngine;

public class UIPokerHand : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pokerName;
    [SerializeField] TextMeshProUGUI poker;
    public int chip = 0;
    public int mul = 0;
    public void UpdatePokerHand(PokerHandType type)
    {
        pokerName.text = type.Name;
        chip = type.BaseChips;
        mul = type.BaseMultiple;
        poker.text = $"{chip} x {mul}" ;
    }
    public void UpdateChip(int chip)
    {
        Debug.Log("_");
        this.chip += chip;
        poker.text = $"{this.chip} x {mul}" ;

    }
    public void UpdateMul(int mul)
    {
        this.mul += mul;
        poker.text = $"{chip} x {this.mul}";

    }
}
