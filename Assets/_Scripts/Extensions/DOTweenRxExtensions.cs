using DG.Tweening;
using System;
using System.Collections.Generic;
using UniRx;

public static class DOTweenRxExtensions
{
    private static readonly Dictionary<string, CompositeDisposable> tweenGroups = new();
    private static readonly Subject<(string groupName, Tween tween)> tweenCompletedSubject = new();
    public static IObservable<Unit> AsObservable(this Tween tween)
    {
        return Observable.Create<Unit>(observer =>
        {
            tween.OnComplete(() =>
            {
                observer.OnNext(Unit.Default);
                observer.OnCompleted();
            });

            tween.OnKill(() =>
            {
                if (tween.IsComplete()) return; 
                observer.OnCompleted(); 
            });

            return Disposable.Create(() =>
            {
                if (tween.IsActive()) tween.Kill();
            });
        });
    }
}
