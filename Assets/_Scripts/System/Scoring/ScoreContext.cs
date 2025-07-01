using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreContext", menuName = "Scriptable Objects/ScoreContext")]
public class ScoreContext : ScriptableObject
{
    public ReactiveProperty<int> RoundScore = new ReactiveProperty<int>();
    public ReactiveProperty<int> targetScore = new();
}
