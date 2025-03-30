
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Balatro.Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;

        public CardDataSO data;
        public CardView uiCard;
        public event Action<Card> OnCardClick;
        public float _moveOffset = 20f;
        //private bool isFlipped = false;
        public bool isSelect;

        public void SetUI(CardDataSO cardData)
        {
            isSelect = false;
            data = cardData;
            valueText.text = $"+{cardData.value}";
            uiCard.SetUI(data);
            uiCard.CardViewClicked += UiCard_CardViewClicked; 

        }

        private void UiCard_CardViewClicked()
        {
            OnCardClick?.Invoke(this);
        }

        //public void FlipCard()
        //{
        //    isFlipped = !isFlipped;
        //    //Sprite sprite = isFlipped ? data.;
        //    StartCoroutine(uiCard.AnimateCardFlip());
        //}
        public void SelectCard(float duration)
        {
            isSelect = true;
            float target = transform.position.y + (_moveOffset);
            transform.DOMoveY(target, duration).SetEase(Ease.OutQuad);
        }
        public void UnSelectCard(float duration)
        {
            isSelect = false;
            float target = transform.position.y - (_moveOffset);
            transform.DOMoveY(target, duration).SetEase(Ease.OutQuad);
        }
        public IEnumerator MoveToPos(Vector3 pos, float duration, bool setActive = true)
        {
            gameObject.SetActive(true);
            transform.DOMove(pos, duration).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(duration);
            gameObject.SetActive(setActive);
            //Destroy(gameObject);
        }
        public IEnumerator ShowValue()
        {
            valueText.gameObject.SetActive(true);
            float target = valueText.transform.position.y + (_moveOffset);
            valueText.transform.DOMoveY(target, 0.2f).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.5f);
            valueText.gameObject.SetActive(false);

        }
    }
}

