using Balatro.Poker;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PokerVariable", menuName = "Scriptable Objects/PokerVariable")]

public class PokerVariable : RSO
{
    PokerHand _poker;
    [SerializeField] IntVariable chip;
    [SerializeField] IntVariable mult;

    public UnityAction<PokerHand> OnValueChanged = delegate { };
    public PokerHand poker
    {
        get => _poker;
        set
        {
            if (value.Equals(_poker)) return;
            _poker = value;

            OnValueChanged?.Invoke(value);
            chip.OnValueChanged?.Invoke(value.Chip);
            mult.OnValueChanged?.Invoke(value.Mult);
        }
    }

    protected override void OnReset()
    {
        OnValueChanged?.Invoke(_poker = PokerHand.None);

    }

}
