using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/BlindRuntimeSO")]
public class BlindRuntimeSO : RuntimeScriptableObject, IBlind
{
    [SerializeField] BlindSO data;
    public BlindSO Data
    {
        get => data;
        set
        {
            //if (data.Equals(value)) return;
            this.data = value;
            onBlindChange?.Invoke(data);
        }
    }
    private int _baseChip;
    private int _blindScore;
    public string Name => data.Name;

    public Sprite Sprite => data.Sprite;

    public int minimunAnte => data.minimunAnte;

    public float scoreMultiple => data.scoreMultiple;

    public int reward => data.reward;

    public int BaseChips
    {
        get => _baseChip;
        set {
            
            this._baseChip = value;
            onBlindChange.Invoke(data);
        }
    }
    public int BlindScore
    {
        get => _blindScore;
        set => _blindScore = value;
    }
    public UnityAction<IBlind> onBlindChange;
    

    protected override void OnReset()
    {
       
    }
}
