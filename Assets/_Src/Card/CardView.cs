using System;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public event Action OnClicked;
    public SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    public void Init(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    public void OnMouseDown()
    {
        OnClicked?.Invoke();
    }

    public void SelectCard()
    {
        Debug.Log("select me");
    }
    public void DeselectCard()
    {
        Debug.Log("deselect me");

    }
    public void DestroyCard()
    {

    }
}
