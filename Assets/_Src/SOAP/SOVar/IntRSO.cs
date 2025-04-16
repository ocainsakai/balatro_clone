using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RSO/IntVariable")]

public class IntRSO : RSO
{
    [SerializeField] int initialValue;
    [SerializeField] int value;

    public UnityAction<int> OnValueChanged = delegate { };
    
    public int Value
    {
        get => value;
        set
        {
            if (this.value == value) return;
            this.value = value;
            OnValueChanged?.Invoke(value);
        }
    }

    protected override void OnReset()
    {
        OnValueChanged?.Invoke(Value = initialValue);
        //value = initialValue;
    }
}
