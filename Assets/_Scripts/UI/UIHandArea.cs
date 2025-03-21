using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 


public class UIHandArea : MonoBehaviour
{
    [SerializeField] private GameObject uiCardPrf;
    
    public List<UICard> cards;


    public float cardSpacing = 1.0f;
    public float cardWidth = 0.8f;
    public float moveDuration = 0.5f;
    private bool isByRank;
    public void SortCardByRank()
    {
        isByRank = true;
        cards = cards.OrderBy(card => card.card.rank).ToList();
        StartCoroutine(UpdateCardPositionsWithAnimation());
    }

    public void SortCardBySuit()
    {
        isByRank = false;
        cards = cards.OrderBy(card => card.card.suit).ToList();
        StartCoroutine(UpdateCardPositionsWithAnimation());
    }

    private IEnumerator UpdateCardPositionsWithAnimation()
    {
        float totalWidth = (cards.Count - 1) * (cardWidth + cardSpacing);

        Vector3[] targetPositions = new Vector3[cards.Count];
        for (int i = 0; i < cards.Count; i++)
        {
            targetPositions[i] = TargetPosition(i);
            if (cards[i].isChoosing)
            {
                targetPositions[i] += Vector3.up * cards[i].moveOffset;
            }
        }

        float elapsedTime = 0f;
        Vector3[] startPositions = cards.Select(card => card.transform.position).ToArray();

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }
            yield return null;
        }

        // Đảm bảo vị trí cuối cùng chính xác
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = targetPositions[i];
        }
    }
    public void OnEnable()
    {
        GameManager.instance.OnDrawCard += DrawCard;
        GameManager.instance.sortBySuit += SortCardBySuit;
        GameManager.instance.sortByRank += SortCardByRank;
        GameManager.instance.OnDiscardCard += Discard;
    }
    
    public void DrawCard(Card card)
    {
        UICard newCard = AddCard().GetComponent<UICard>();
        newCard.Initlize(card);
        newCard.OnChoosingCard += GameManager.instance.Choosing;
        cards.Add(newCard); 

    }
    public void Discard(Card card)
    {
        StartCoroutine(DiscardCoroutine(card));
    }

    public IEnumerator DiscardCoroutine(Card choosing_cards)
    {
       
        UICard card = cards.FirstOrDefault(card => card.card == choosing_cards);
        cards.Remove(card);
        Destroy(card.gameObject);
        yield return new WaitForSeconds(0.2f);
    }
    private GameObject AddCard()
    {
        GameObject newCard = Instantiate(uiCardPrf);
        newCard.transform.SetParent(transform);
        newCard.transform.localScale = Vector3.one;
        newCard.transform.position = TargetPosition(cards.Count);
        newCard.SetActive(true);
        return newCard;
    }
    private Vector3 TargetPosition(int i)
    {   float startX = transform.position.x - GetComponent<SpriteRenderer>().size.x/2 + cardWidth;
        return new Vector3(
              startX +  i * (cardWidth + cardSpacing),0,0
            );
    }
}
