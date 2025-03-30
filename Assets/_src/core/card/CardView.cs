using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace Balatro.Cards
{
    // Script quản lý giao diện thẻ bài
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        [Header("Card UI Components")]
        [SerializeField] private Image cardBackgroundImage;
        [SerializeField] private Image cardMainImage;

        [Header("Card Flip Animation")]
        [SerializeField] private float flipAnimationDuration = 0.5f;
        [SerializeField] private AnimationCurve flipCurve;

        [Header("Card Sprites")]
        [SerializeField] private Sprite defaultCardBack;
        [SerializeField] private Sprite highlightedCardBack;

        //private RectTransform rectTransform;
        //public CardDataSO currentCardData { get; private set; }

        public event Action CardViewClicked;

        public void SetUI(CardDataSO cardData)
        {
            cardMainImage.sprite = cardData.cardSprite;
            cardBackgroundImage.color = cardData.primaryColor;
            //valueText.text ="+" +cardData.value.ToString();
            //valueText.gameObject.SetActive(false);
        }

        //private IEnumerator AnimateCardFlip(Sprite cardSprite)
        //{
        //    float elapsedTime = 0f;
        //    Quaternion startRotation = transform.rotation;
        //    Quaternion endRotation = Quaternion.Euler(0, 180f, 0);

        //    while (elapsedTime < flipAnimationDuration)
        //    {
        //        elapsedTime += Time.deltaTime;
        //        float percentageComplete = elapsedTime / flipAnimationDuration;

        //        // Sử dụng animation curve để làm mượt chuyển động
        //        float curveValue = flipCurve.Evaluate(percentageComplete);
        //        transform.rotation = Quaternion.Lerp(startRotation, endRotation, curveValue);

        //        // Thay đổi sprite khi ở giữa quá trình lật
        //        if (percentageComplete >= 0.5f)
        //        {
        //            cardMainImage.sprite = cardSprite;
        //        }

        //        yield return null;
        //    }

        //    transform.rotation = endRotation;
        //}
        public void OnPointerClick(PointerEventData eventData)
        {
            CardViewClicked?.Invoke();
        }
        
    }
    
}