using UnityEngine;
using UnityEngine.UIElements;

public abstract class IntVarDisplayAbstract : MonoBehaviour
{
    protected UIDocument document;
    protected Label label;
    [SerializeField] protected IntVariable intVariable;


    protected void Awake()
    {
        document = GetComponent<UIDocument>();
    }
    protected virtual void OnEnable()
    {
        
    }
    protected virtual void OnDisable()
    {

    }
    protected virtual void UpdateDisPlay(int value)
    {

    }
}
