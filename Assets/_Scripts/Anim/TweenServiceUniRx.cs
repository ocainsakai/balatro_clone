using System;
using System.Collections.Generic;
using UniRx;
using DG.Tweening;
using UnityEngine;

public class TweenServiceUniRx : MonoBehaviour
{
    private static TweenServiceUniRx _instance;
    public static TweenServiceUniRx Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<TweenServiceUniRx>();
                if (_instance == null)
                {
                    var go = new GameObject("TweenServiceUniRx");
                    _instance = go.AddComponent<TweenServiceUniRx>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private readonly Dictionary<string, CompositeDisposable> tweenGroups = new();
    private readonly Subject<(string groupName, Tween tween)> tweenCompletedSubject = new();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public IObservable<Unit> PlayTween(string groupName, Tween tween)
    {
        if (!tweenGroups.ContainsKey(groupName))
            tweenGroups[groupName] = new CompositeDisposable();

        var disposable = Disposable.Create(() =>
        {
            if (tween.IsActive()) tween.Kill();
        });

        tweenGroups[groupName].Add(disposable);

        return tween.AsObservable()
            .DoOnSubscribe(() => tween.Play())
            .DoOnCompleted(() => tweenCompletedSubject.OnNext((groupName, tween)))
            .DoOnTerminate(disposable.Dispose)
            .AsUnitObservable();
    }

    public void PauseGroup(string groupName)
    {
        if (!tweenGroups.TryGetValue(groupName, out var group)) return;

        foreach (var disposable in group)
        {
            if (disposable is Tween tween && tween.IsActive())
                tween.Pause();
        }
    }

    public void ResumeGroup(string groupName)
    {
        if (!tweenGroups.TryGetValue(groupName, out var group)) return;

        foreach (var disposable in group)
        {
            if (disposable is Tween tween && tween.IsActive())
                tween.Play();
        }
    }

    public void StopGroup(string groupName)
    {
        if (!tweenGroups.TryGetValue(groupName, out var group)) return;

        group.Dispose();
        tweenGroups.Remove(groupName);
    }

    public void StopAllTweens()
    {
        foreach (var group in tweenGroups.Values)
            group.Dispose();

        tweenGroups.Clear();
    }

    public IObservable<(string groupName, Tween tween)> OnTweenCompleted() =>
        tweenCompletedSubject.AsObservable();

    public IObservable<Unit> WhenGroupCompleted(string groupName)
    {
        if (!tweenGroups.ContainsKey(groupName))
            return Observable.ReturnUnit();

        return OnTweenCompleted()
            .Where(x => x.groupName == groupName)
            .Take(tweenGroups[groupName].Count)
            .AsUnitObservable();
    }

    private void OnDestroy()
    {
        StopAllTweens();
        tweenCompletedSubject.Dispose();

        if (_instance == this)
            _instance = null;
    }
}
