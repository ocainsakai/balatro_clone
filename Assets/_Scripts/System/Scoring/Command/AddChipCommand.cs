using System;
using UnityEngine;

[Serializable]
public class AddChipCommand : IPlayEffectCommand
{
    [SerializeField]
    private int _amount;
    public void SetAmount(int amount)
    {
        _amount = amount;
    }
    public void Execute(object input, params object[] output)
    {
        var card = input as Card;
        if (card != null )
        {
            var poker = output[0] as PokerContext;
            if ( poker != null)
            {
                poker.Chip.Value += _amount;
            }
        }

    }
}