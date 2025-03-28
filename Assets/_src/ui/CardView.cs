using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using TMPro;
using System.Collections;

namespace Balatro.Cards
{
    // Script quản lý giao diện thẻ bài
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        [Header("Card UI Components")]
        [SerializeField] private Image cardBackgroundImage;
        [SerializeField] private Image cardMainImage;
        [SerializeField] private TextMeshProUGUI valueText;

        [Header("Card Flip Animation")]
        [SerializeField] private float flipAnimationDuration = 0.5f;
        [SerializeField] private AnimationCurve flipCurve;

        [Header("Card Sprites")]
        [SerializeField] private Sprite defaultCardBack;
        [SerializeField] private Sprite highlightedCardBack;

        //private RectTransform rectTransform;
        public CardDataSO currentCardData { get; private set; }

        public event Action<CardView> CardViewClicked;

        public float _moveOffset = 20f;
        private bool isFlipped = false;
        public bool isSelect;
        private void Awake()
        {
            //rectTransform = GetComponent<RectTransform>();
        }

        // Thiết lập thông tin cho thẻ
        public void SetCardData(CardDataSO cardData)
        {
            isSelect = false;
            currentCardData = cardData;
            cardMainImage.sprite = cardData.cardSprite;
            cardBackgroundImage.color = cardData.primaryColor;
            valueText.text ="+" +cardData.value.ToString();
            valueText.gameObject.SetActive(false);

        }

        public void FlipCard()
        {
            isFlipped = !isFlipped;
            StartCoroutine(AnimateCardFlip());
        }

        // Coroutine hoạt ảnh lật bài
        private System.Collections.IEnumerator AnimateCardFlip()
        {
            float elapsedTime = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(0, 180f, 0);

            while (elapsedTime < flipAnimationDuration)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / flipAnimationDuration;

                // Sử dụng animation curve để làm mượt chuyển động
                float curveValue = flipCurve.Evaluate(percentageComplete);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, curveValue);

                // Thay đổi sprite khi ở giữa quá trình lật
                if (percentageComplete >= 0.5f)
                {
                    cardMainImage.sprite = isFlipped ? defaultCardBack : currentCardData.cardSprite;
                }

                yield return null;
            }

            transform.rotation = endRotation;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            CardViewClicked?.Invoke(this);
        }
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
        public IEnumerator MoveToPos(Vector3 pos, float duration)
        {
            transform.DOMove(pos, duration).SetEase(Ease.OutQuad);
            yield return new WaitForSeconds(0.2f);
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

    // Factory để tạo card prefab
    
}