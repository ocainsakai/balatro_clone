using System;
using UnityEngine;

public class UICard : MonoBehaviour
{
    public event Action<Card, bool> OnChoosingCard;
    public Card card { get; private set; }
    [SerializeField] public float moveOffset = 0.3f;
    public bool isChoosing {get; private set;}
    public void OnMouseDown()
    {
        if (GameManager.instance.canChoose || isChoosing)
        {
            isChoosing = !isChoosing;
            this.transform.position +=  Vector3.up * (isChoosing ? moveOffset : -moveOffset);
            OnChoosingCard?.Invoke(card, isChoosing);
        }
    }
    
    public void Initlize(Card card)
    {
        this.card = card;
        this.isChoosing = false;
        GetComponent<SpriteRenderer>().sprite = card.cardFrontImage;
    }
}
