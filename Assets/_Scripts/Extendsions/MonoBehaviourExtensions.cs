using System;
using UniRx;
using UnityEngine;

public static class MonoBehaviourExtensions
{
   
    public static IObservable<int> ObserveChildCount(this Transform transform)
    {
        return Observable.EveryUpdate()
            .Select(_ => transform.childCount)
            .DistinctUntilChanged();
    }
    
}