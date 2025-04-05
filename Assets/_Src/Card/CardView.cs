using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


namespace Card
{
    public class CardView : MonoBehaviour, ICard
    {
        public event Action OnClicked;
        private ICard data;
        public string Name => data.Name;

        public Sprite sprite => data.sprite;

       
        public void RenderArt(ICard card)
        {
            this.data = card;
            GetComponent<SpriteRenderer>().sprite = sprite;
            
        }
        public void OnSelect()
        {
            transform.DOMoveY(0.8f, 0.2f).SetEase(Ease.OutQuad);
        }
        public void OnUnselect()
        {
            transform.DOMoveY(0f, 0.2f).SetEase(Ease.OutQuad);
        }
        public void OnMouseDown()
        {
            OnClicked?.Invoke();
        }
    }
}

