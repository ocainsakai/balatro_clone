using System;
using UnityEngine;


public class UICard : MonoBehaviour
{
    public Card card { get; private set; }
    [SerializeField] public float moveOffset = 0.3f;
    public bool isChoosing;
    public void Initlize(Card card)
    {
        this.card = card;
        this.isChoosing = false;
        GetComponent<SpriteRenderer>().sprite = card.cardFrontImage;
    }
    private void OnMouseDown()
    {
        if (isChoosing)
        {
            PlayingManager.instance.UnSelect(card);
            isChoosing = false;
            this.transform.position -= Vector3.up * moveOffset;
        } else
        {
            isChoosing = PlayingManager.instance.Select(card);
            if(isChoosing )
            this.transform.position += Vector3.up * moveOffset;
        }      
    }
}