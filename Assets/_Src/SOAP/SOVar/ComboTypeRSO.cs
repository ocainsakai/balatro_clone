using Balatro.Cards;
using Balatro.Combo;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RSO/ComboVariable")]

public class ComboTypeRSO : RSO
{
    [SerializeField] public ComboTypeData None;
    private ComboTypeData _comboType;
    public IntRSO chip;
    public IntRSO mult;
    public List<Card> comboCard;
    public ComboTypeData ComboType
    {
        get => _comboType;
        set
        {
            if (value != _comboType)
            {
                _comboType = value;
                chip.Value = value.Chip;
                mult.Value = value.Mult;
                OnComboTypeChange?.Invoke(value);
            }
        }
    }
    public event UnityAction<ComboTypeData> OnComboTypeChange = delegate { };
    public void ResetCombo()
    {
        ComboType = None;
    }
    protected override void OnReset()
    {
        ResetCombo();
    }
}
