using DG.Tweening;
using System;
using UniRx;
using UnityEngine;

public class CardView : MonoBehaviour
{
    private ReactiveCommand<Unit> _onClicked;
    public IReactiveCommand<Unit> OnClicked => _onClicked;
    public SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    public void Init(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
    private void Awake()
    {
        _onClicked = new ReactiveCommand<Unit>();
    }
    public void OnMouseDown()
    {

        _onClicked.Execute(Unit.Default);
    }

    public void SelectCard()
    {
        Debug.Log("on select");
        spriteRenderer.color = Color.blue;
        transform?.DOScale(Vector3.one * 1.1f, 0.3f).SetEase(Ease.OutBack);
    }
    public void DeselectCard()
    {
        spriteRenderer.color = Color.white;
        transform?.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBack);

    }
    public void DestroyCard()
    {
        transform.SetParent(null);
        Destroy(gameObject);
    }
}
