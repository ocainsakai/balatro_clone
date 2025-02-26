using UnityEngine;

public class CardColliderHandler : MonoBehaviour
{
    public Card ctrl => GetComponentInParent<Card>();
    public void OnMouseDown()
    {
        ctrl.Choose();
    }
}
