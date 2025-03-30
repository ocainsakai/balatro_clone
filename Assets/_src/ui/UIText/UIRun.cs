using TMPro;
using UnityEngine;

public class UIRun : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _roundScore;
    [SerializeField] TextMeshProUGUI _pokerName;
    [SerializeField] TextMeshProUGUI _pokerChip;
    [SerializeField] TextMeshProUGUI _pokerMul;
    [SerializeField] TextMeshProUGUI _handCount;
    [SerializeField] TextMeshProUGUI _discardCount;
    [SerializeField] TextMeshProUGUI _money;
    [SerializeField] TextMeshProUGUI _ante;
    [SerializeField] TextMeshProUGUI _roundIndex;

    public void UpdateRoundScore(int score)
    {
        if (_roundScore != null)
            _roundScore.text = "Round score: " + score.ToString();
    }

    public void UpdatePokerName(string name)
    {
        if (_pokerName != null)
            _pokerName.text = name;
    }

    public void UpdatePokerChip(int chips)
    {
        if (_pokerChip != null)
            _pokerChip.text = chips.ToString();
    }

    public void UpdatePokerMul(int multiplier)
    {
        if (_pokerMul != null)
            _pokerMul.text = multiplier.ToString();
    }

    public void UpdateHandCount(int count)
    {
        if (_handCount != null)
            _handCount.text = "Hand count: " + count.ToString();
    }

    public void UpdateDiscardCount(int count)
    {
        if (_discardCount != null)
            _discardCount.text = "Discard: " + count.ToString();
    }

    public void UpdateMoney(int amount)
    {
        if (_money != null)
            _money.text = "$" + amount.ToString();
    }

    public void UpdateAnte(int ante)
    {
        if (_ante != null)
            _ante.text = "Ante: " + ante.ToString();
    }

    public void UpdateRoundIndex(int index)
    {
        if (_roundIndex != null)
            _roundIndex.text = "Round " + index.ToString();
    }
}
