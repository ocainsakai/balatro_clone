
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class JokerCard : MonoBehaviour {
    protected JokerManager Manager => FindFirstObjectByType<JokerManager>();
    //protected ScoreManager ScoreManager => FindFirstObjectByType<ScoreManager>();
    // property
    protected JokerCardData Data;
    protected CardView View;
    //protected 
    // state
    // behaviour
    protected virtual void Awake()
    {
        if (Data == null)
        {
            Data = Resources.Load<JokerCardData>($"GameData/Joker/{GetType().Name.AddWhiteSpace()}");
        }
        View = GetComponent<CardView>();
    }

    public virtual UniTask PostActive()
    {
        return UniTask.CompletedTask;
    }
    public virtual UniTask OnScoredActive(Card card)
    {
        return UniTask.CompletedTask;
    }
}
