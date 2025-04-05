using UnityEngine;
using UnityEngine.Events;
public interface IGameEventListener<T>
{
    void OnEventRaised(T data);
}
public class GameEventListener<T> : MonoBehaviour, IGameEventListener<T>
{
    [SerializeField] GameEvent<T> gameEvent;
    [SerializeField] UnityEvent<T> response;

    private void OnEnable() => gameEvent.RegisterListener(this);
    private void OnDisable() => gameEvent.DegisterListener(this);

    public void OnEventRaised(T data) => response.Invoke(data);
}
