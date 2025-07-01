using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "PokerContext", menuName = "Scriptable Objects/PokerContext")]
public class PokerContext : ScriptableObject
{
    public ReactiveProperty<PokerHandType> HandType = new(0);
    public ReactiveProperty<int> Chip = new ReactiveProperty<int>();
    public ReactiveProperty<int> Mult = new ReactiveProperty<int>();
    public List<Card> IsScoredCard = new List<Card>();
    private CompositeDisposable disposables = new();

    private void OnEnable()
    {
        HandType.Subscribe(_ =>
        {
            Chip.Value = _.GetPokerData().Chip;
        }).AddTo(disposables);
        HandType.Subscribe(_ =>
        {
            Mult.Value = _.GetPokerData().Mult;
        }).AddTo(disposables);
        OnReset();
    }
    private void OnDisable()
    {
        disposables.Clear();

    }
    public void OnReset()
    {
        HandType.Value = 0;
    }
}
