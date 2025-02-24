using UnityEngine;

public class CardActionHandler : MonoBehaviour
{
    public Card ctrl => GetComponentInParent<Card>();
    public void OnMouseDown()
    {
        ctrl.Choose();
    }
}
