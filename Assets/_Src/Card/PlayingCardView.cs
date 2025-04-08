
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Balatro.Card
{
    public class PlayingCardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] public Image _image;
        RectTransform _rectTransform => GetComponent<RectTransform>();
        public Action OnClick;

        float initY;

        public void SetInit(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
        public void OnSelect()
        {
            //initY = GetComponent<RectTransform>().anchoredPosition.y;
            _rectTransform.DOAnchorPosY(20f, 0.2f).SetEase(Ease.OutQuad);
        }
        public void OnUnselect()
        {
            _rectTransform.DOAnchorPosY(0f,  0.2f).SetEase(Ease.OutQuad);
        }
        public void OnMouseDown()
        {
            OnClick?.Invoke();
        }
        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    // Dừng và hủy sequence cũ nếu có
        //    if (shake != null)
        //    {
        //        shake.Kill();
        //    }

        //    shake = transform.DOShakePosition(
        //    duration: 10f,       // Thời gian rung (lặp vô hạn nên không quan trọng lắm)
        //    strength: 5f,       // Độ mạnh của rung (nhẹ nhàng)
        //    vibrato: 10,        // Số lần rung mỗi giây
        //    randomness: 90,     
        //    fadeOut: false
        //    ).SetLoops(-1);
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    if (shake != null)
        //    {
        //        shake.Kill();
        //        shake = null;
        //    }

        //    // Đặt lại vị trí ban đầu (nếu cần)
        //    transform.localPosition = Vector3.zero;
        //}
    }

}
