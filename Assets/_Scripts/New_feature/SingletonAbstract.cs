using UnityEngine;

public abstract class SingletonAbstract<T> : MonoBehaviour where T : SingletonAbstract<T>
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
