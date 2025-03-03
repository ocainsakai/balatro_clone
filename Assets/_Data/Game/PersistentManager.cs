using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance;

    public GameManager gameManager;
    //public AudioManager audioManager;
    //public SaveManager saveManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void ResetPersistentObjects()
    {
        GameObject[] persistentObjects = GameObject.FindGameObjectsWithTag("Persistent");
        foreach (var obj in persistentObjects)
        {
            Destroy(obj);
        }
    }

}
