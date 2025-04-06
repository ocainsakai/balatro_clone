using System.Collections.Generic;
using UnityEngine;


public abstract class RSO : ScriptableObject
{
    readonly static List<RSO> Instances = new List<RSO>();

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
