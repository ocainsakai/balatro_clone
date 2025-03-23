using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;


public class UIHandZone : MonoBehaviour
{
    [SerializeField] private GameObject uiCardPrf;
    [SerializeField] private Transform deckPosition;

    public List<UICard> cards;

    public float cardWidth = 0.75f;
    public float cardSpace = 0.15f;
    public float effectDuration = 0.3f;

    public bool isSortByRank;

    private void Start()
    {
        isSortByRank = true;
    }
    UICard CreateCard(Card card)
    {
        UICard newCard = Instantiate(uiCardPrf).GetComponent<UICard>();
        cards.Add(newCard);
        newCard.Initlize(card);
        //newCard.OnChoosingCard += PlayingManager.instance.Choosing;
        newCard.transform.SetParent(transform);
        newCard.transform.localScale = Vector3.one;
        //newCard.transform.position = TargetPositionX(cards.Count);
        newCard.gameObject.SetActive(true);
        return newCard;
    }
    public IEnumerator Sort(int kind = 0)
    {
        if (kind != 0) isSortByRank = kind % 2 == 0;
        if (isSortByRank)
        {
            cards = cards.OrderBy(x => x.card.rank).ToList();
        }
        else
        {
            cards = cards.OrderBy(x => x.card.suit).ToList();
        }
        for (int i = 0; i < cards.Count; i++)
        {
            UICard card = cards[i];
            card.isChoosing = false;
            PlayingManager.instance.UnSelect(card.card);
            Vector3 start = card.transform.position;
            Vector3 target = Target(i);
            card.transform.DOMove(target, effectDuration).SetEase(Ease.InOutQuad);
        }
        yield return new WaitForSeconds(effectDuration);
    }
    public IEnumerator AddCardCoroutine(Card card)
    {
        UICard newCard = CreateCard(card);

        // Đặt vị trí ban đầu là deck
        newCard.transform.position = deckPosition.position;

        // Xác định vị trí đích (ví dụ dựa trên số lượng card hiện tại)
        Vector3 targetPosition = Target(cards.Count - 1); // -1 vì vừa thêm card
        newCard.transform.DOMove(targetPosition, effectDuration).SetEase(Ease.InOutQuad);

        // Chờ cho đến khi di chuyển xong
        yield return new WaitForSeconds(effectDuration);
    }
    public IEnumerator DiscardCoroutine(Card card)
    {
        UICard uiCard = cards.FirstOrDefault(x => x.card == card);


        // Thu nhỏ card về 0 cùng lúc với rung lắc
        uiCard.transform.DOScale(0f, effectDuration).SetEase(Ease.InOutQuad);
        uiCard.transform.DOShakePosition(effectDuration, strength: 0.1f, vibrato: 10, randomness: 90f);

        // Chờ cho hiệu ứng hoàn tất
        yield return new WaitForSeconds(effectDuration);
        cards.Remove(uiCard);
        Destroy(uiCard.gameObject);
    }
    Vector3 Target(int index)
    {
        Vector3 startPoint = transform.position;
        return startPoint + Vector3.right * (cardWidth+cardSpace) * index; 
    }

}
