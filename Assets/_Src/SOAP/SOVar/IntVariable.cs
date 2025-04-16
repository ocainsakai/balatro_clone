using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO Variable/IntVariable")]

public class IntVariable : RSO
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
