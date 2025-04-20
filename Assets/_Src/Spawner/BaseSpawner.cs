using UnityEngine;

public abstract class BaseSpawner<TData, T> : MonoBehaviour, ISpawner<TData, T>
{
    [SerializeField] protected Transform _prefab;
    [SerializeField] protected Transform _container;
    public abstract T Create(TData data);
}
