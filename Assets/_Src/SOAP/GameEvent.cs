using System.Collections.Generic;
using UnityEngine;


public class GameEvent<T> : ScriptableObject
{
    readonly List<IGameEventListener<T>> listeners = new ();


    public void Raise(T data)
    {
        for (int i = listeners.Count-1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }
    public void RegisterListener(IGameEventListener<T> listener) { listeners.Add(listener); }
    public void DegisterListener(IGameEventListener<T> listener) { listeners.Remove(listener); }
}
[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]

public class GameEvent : GameEvent<Unit>
{
    public void Raise() => Raise(Unit.Default);
}
public struct Unit
{
    public static Unit Default => default;
}