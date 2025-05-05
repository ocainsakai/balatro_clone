using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    private static readonly Dictionary<MonoBehaviour, Process> processInstances = new Dictionary<MonoBehaviour, Process>();
    public static void AddProcess(this MonoBehaviour mono, IEnumerator enumerator)
    {
        if (!processInstances.ContainsKey(mono))
        {
            processInstances[mono] = new Process();
        }
        processInstances[mono].Anim(mono, enumerator);
    }

    public static void ClearQueue(this MonoBehaviour mono)
    {
        if (processInstances.ContainsKey(mono))
        {
            processInstances[mono].ClearQueue();
        }
    }

    public static void OnDestroy(this MonoBehaviour mono)
    {
        if (processInstances.ContainsKey(mono))
        {
            processInstances[mono].ClearQueue();
            processInstances.Remove(mono);
        }
    }
}