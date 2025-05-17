using Game.Cards;
using UnityEngine;
using UniRx;
using DG.Tweening;
public class CardArt : MonoBehaviour
{
    [SerializeField] Sprite cardBack;
    public SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();

    public Card _card;
    public void SetCard(Card card)
    {
        this._card = card;
        card.IsFlip.Subscribe(_ => UpdateImage());
        card.State.Subscribe(x =>
        {
            if (x == CardState.Hold || x == CardState.Play)
            {
                transform.DOLocalMoveY(0, 0.1f);
            }
            else if (x == CardState.Select || x == CardState.Score)
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
            Card.Select.OnNext(_card);
        }
        else if (_card.State.Value == CardState.Hold && Card.CanSelect)
        {
            _card.State.Value = CardState.Select;
            Card.Select.OnNext(_card);
        }
    }
    public void UpdateImage()
    {
        _spriteRenderer.sprite = _card.IsFlip.Value ? _card.Data.Artwork : cardBack;
    }

}