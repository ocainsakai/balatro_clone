using Balatro.Combo;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO Variable/ComboVariable")]

public class ComboVariable : RSO
{
    [SerializeField] public ComboTypeData None;
    private ComboTypeData _comboType;
    public IntVariable chip;
    public IntVariable mult;
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
