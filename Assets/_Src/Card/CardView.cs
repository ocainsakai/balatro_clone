using Balatro.Cards.CardsRuntime;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textValue;
    public event Action<CardView> OnSelected;

    public bool isSelected =  false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textValue = GetComponentInChildren<TextMeshPro>();
    }
    public void Setup(Sprite sprite, int value)
    {
        spriteRenderer.sprite = sprite;
        textValue.text = $"+{value}";
        textValue.gameObject.SetActive(false);
    }
    public void OnMouseDown()
    {
        OnSelected?.Invoke(this);
    }
    public void OnSeleted()
    {
        isSelected = true;
        transform.DOMoveY(1.5f, 0.2f);
    }
    public void OnUnseleted()
    {
        isSelected = false;
        transform.DOMoveY(0, 0.2f);
    }
    public void OnRemoved()
    {
        transform.DOScale(0, 0.2f).OnComplete(
            () =>
            {
                Destroy(gameObject);
            });
    }
}
