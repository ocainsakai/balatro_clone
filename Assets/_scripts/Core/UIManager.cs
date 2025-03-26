
using UnityEngine;

public class UIManager : SingletonAbs<UIManager>
{
    [SerializeField] private UIPokerHand ui_poker_hand;

    public void NonePoker()
    {
        ui_poker_hand.UpdatePokerHand(Poker.none);
    }
    public void UpdatePokerHand(Poker poker)
    {
        ui_poker_hand.UpdatePokerHand(poker);

    }
    public void UpdateChip(int value)
    {
        ui_poker_hand.UpdateChip(value, 0.2f);
    }
}
