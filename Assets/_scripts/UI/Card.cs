using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Card : MonoBehaviour, IPointerClickHandler
{
    public CardSO cardSO { get; private set; }
    //[SerializeField] public float moveOffset = 20f;
    [SerializeField] TextMeshProUGUI plus_score;

    public event Action<Card> OnSelect;
    //public event Action<Card> OnUnSelect;
    public bool isChoosing;

    public void Initlize(CardSO card)
    {
        this.cardSO = card;
        this.isChoosing = false;
        plus_score.gameObject.SetActive(false);
        GetComponent<Image>().sprite = card.cardFrontImage;
    }
 
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect?.Invoke(this);
    }
    public IEnumerator PlusCoroutine()
    {
        plus_score.text = $"+{cardSO.baseValue}";
        plus_score.color = Color.green;
        plus_score.gameObject.SetActive (true);

        float duration = 0.5f;
        Vector3 targetPos = plus_score.transform.position + Vector3.up * 50f;
        plus_score.transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad);
        plus_score.DOFade(0f, duration).SetEase(Ease.InQuad); // Mờ dần
        UIHand.instance.ui_poker_hand.UpdateChip(cardSO.baseValue, duration);
        //GameManager.instance.;
        yield return new WaitForSeconds(duration + 0.1f);
        
        plus_score.gameObject.SetActive(false);
    }
}