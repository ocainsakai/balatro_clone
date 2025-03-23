using UnityEngine;

public abstract class SingletonAbs<T> : MonoBehaviour where T : SingletonAbs<T>
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
