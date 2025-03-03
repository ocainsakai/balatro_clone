using UnityEngine;
using UnityEngine.EventSystems;

public class CardColliderHandler : MonoBehaviour, IPointerClickHandler
{
    public Card ctrl => GetComponentInParent<Card>();
    public void OnPointerClick(PointerEventData eventData)
    {
        ctrl.Choose();
    }
}
