using UnityEngine;

public interface ISpawner <TData, T>
{
    T Create(TData data);
}
