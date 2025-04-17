using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager : MonoBehaviour, IManager
{
    private static readonly List<BaseManager> instances = new();

    public static IReadOnlyList<IManager> Instances => instances;

    protected virtual void Awake()
    {
        if (instances.Exists(x => x.GetType() == this.GetType()))
        {
            Debug.LogWarning($"Duplicate manager of type {this.GetType().Name} detected.");
        }

        if (!instances.Contains(this))
            instances.Add(this);
    }

    protected virtual void OnDestroy()
    {
        instances.Remove(this);
    }
    public static T GetManager<T>() where T : BaseManager
    {
        var manager = instances.Find(x => x is T) as T;

        if (manager == null)
        {
            Debug.LogWarning($"[Manager] Can't not find {typeof(T).Name}. Sure create at scene");
        }

        return manager;
    }

    public virtual void Initialize() { }
    public virtual void ResetManager() { }
    public virtual void Cleanup() { }
}

