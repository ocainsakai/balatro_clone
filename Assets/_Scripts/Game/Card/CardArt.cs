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
            if (x == CardState.OnHand)
            {
                transform.DOLocalMoveY(0, 0.1f);
            }
            else if (x == CardState.Selected)
            {
                transform.DOLocalMoveY(0.5f, 0.1f);
            }
        });
        UpdateImage();
    }
    public void OnMouseDown()
    {
        if (_card.State.Value == CardState.Selected)
        {
            _card.State.Value = CardState.OnHand;
        }
        else if (_card.State.Value == CardState.OnHand && _card.CanSelect)
        {
            _card.State.Value = CardState.Selected;

        }
    }
    public void UpdateImage()
    {
        _spriteRenderer.sprite = _card?.Data.Artwork ?? null;
    }

}