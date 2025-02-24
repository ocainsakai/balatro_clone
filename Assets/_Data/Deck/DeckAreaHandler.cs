using UnityEngine;

public class DeckAreaHandler : MonoBehaviour
{
    public float spacing = 2.0f; 

    void Start()
    {
        ArrangeChildren();
    }
    void OnTransformChildrenChanged()
    {
        ArrangeChildren();
    }

    void ArrangeChildren()
    {
        int index = 0;
        foreach (Transform child in transform)
        {
            child.localPosition = new Vector3(index * spacing, 0, 0);
            index++;
        }
    }

}
