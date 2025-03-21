using UnityEngine;

public abstract class AbstractSingleton<T> : MonoBehaviour where T : AbstractSingleton<T>
{
    public static T instance;
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
