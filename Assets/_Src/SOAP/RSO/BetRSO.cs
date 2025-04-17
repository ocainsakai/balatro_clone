using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RSO/Bet RSO")]
public class BetRSO : RSO
{
    [SerializeField] BetOption init;
    [SerializeField] IntRSO blind;
    private BetOption _option;
    public BetOption Option
    {
        get => _option;
        set {
            _option = value;
            blind.Value = value.requiredScore;
            OnOptionChanged?.Invoke(value);
        }
    }
    public event UnityAction<BetOption> OnOptionChanged;
    protected override void OnReset()
    {
        Option = init;
    }
}
