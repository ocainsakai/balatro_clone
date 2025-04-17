
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace Balatro.Cards.UI
{
    public class CardView : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private TextMeshPro textValue;
        public event Action<CardView> OnSelected;

        public bool isSelected = false;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            textValue = GetComponentInChildren<TextMeshPro>();
        }
        private void OnDestroy()
        {
            OnSelected -= OnSelected;
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
        public void Select()
        {
            isSelected = true;
            transform.DOMoveY(1.5f, 0.2f);
        }
        public void Deselect()
        {
            isSelected = false;
            transform.DOMoveY(0, 0.2f);
        }
        public void OnScore()
        {
            textValue.gameObject.SetActive(true);
            textValue.GetComponent<RectTransform>().DOAnchorPosY(2f, 0.2f);
        }
    }
}

