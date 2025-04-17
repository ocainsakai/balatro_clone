using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "RSO/Bet RSO")]
public class BetRSO : RSO
{
    [SerializeField] BetOption init;
    private BetOption _option;
    public BetOption Option
    {
        get => _option;
        set {
            _option = value;
            OnOptionChanged?.Invoke(value);
        }
    }
    public event UnityAction<BetOption> OnOptionChanged;
    protected override void OnReset()
    {
        Option = init;
    }
}
