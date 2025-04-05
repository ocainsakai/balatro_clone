using System.Collections.Generic;
using UnityEngine;


public abstract class RuntimeScriptableObject : ScriptableObject
{
    readonly static List<RuntimeScriptableObject> Instances = new List<RuntimeScriptableObject>();

    private void OnEnable()
        => Instances.Add(this);
    private void OnDisable()
    {
        Instances.Remove(this);
    }
    protected abstract void OnReset();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResetAllInstaces ()
    {
        foreach (var instance in Instances)
        {
            instance.OnReset();
        }
    }

}
