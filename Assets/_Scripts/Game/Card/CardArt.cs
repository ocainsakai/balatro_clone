using Game.Cards;
using UnityEngine;
using UniRx;
using DG.Tweening;
public class CardArt : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    public Card _card;
    public void SetCard(Card card)
    {
        this._card = card;
        card.State.Subscribe(x =>
        {
            if (x == CardState.Hold)
            {
                transform.DOLocalMoveY(0, 0.1f);
            }
            else if (x == CardState.Select)
            {
                transform.DOLocalMoveY(0.5f, 0.1f);
            }
        });
        UpdateImage();
    }
    public void OnMouseDown()
    {
        if (_card.State.Value == CardState.Select)
        {
            _card.State.Value = CardState.Hold;
        }
        else if (_card.State.Value == CardState.Hold && _card.CanSelect)
        {
            _card.State.Value = CardState.Select;

        }
    }
    public void UpdateImage()
    {
        _spriteRenderer.sprite = _card?.Data.Artwork ?? null;
    }

}