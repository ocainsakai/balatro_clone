using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
 


public class HandManager : SingletonAbstract<HandManager>
{
    [SerializeField] private GameObject uiCardPrf;
    
    public List<UICard> cards;


    public float cardSpacing = 1.0f;
    public float cardWidth = 0.8f;
    public float moveDuration = 0.5f;
    //private bool isByRank;
    
    public void OnEnable()
    {
        PlayingManager.instance.OnDrawCard += DrawCard;
        PlayingManager.instance.OnSort += Sort;
        PlayingManager.instance.OnDiscardCard += Discard;
    }
    public void OnDisable()
    {
        PlayingManager.instance.OnDrawCard -= DrawCard;
        PlayingManager.instance.OnSort -= Sort;
        PlayingManager.instance.OnDiscardCard -= Discard;
    }
    private GameObject AddCard()
    {
        GameObject newCard = Instantiate(uiCardPrf);
        newCard.transform.SetParent(transform);
        newCard.transform.localScale = Vector3.one;
        //newCard.transform.position = TargetPositionX(cards.Count);
        newCard.SetActive(true);
        return newCard;
    }
    private float TargetPositionX(int i)
    {   
        float startX = transform.position.x - GetComponent<SpriteRenderer>().size.x/2 + cardWidth;
        return
              startX + i * (cardWidth + cardSpacing);
            
    }
    public void Sort(int index = 0)
    {
        if (index == 1)
        {
            cards = cards.OrderBy(card => card.card.rank).ToList();
        }
        else if (index == 2)
        {
            cards = cards.OrderBy(card => card.card.suit).ToList();

        }
        //StartCoroutine(UpdateCardPositionsWithAnimation());

    }

    public void DrawCard(Card card)
    {
        UICard newCard = AddCard().GetComponent<UICard>();
        newCard.Initlize(card);
        newCard.OnChoosingCard += PlayingManager.instance.Choosing;
        cards.Add(newCard); 

    }
    public void Discard(Card card)
    {
        StartCoroutine(DiscardCoroutine(card));
    }
    //private IEnumerator UpdateCardPositionsWithAnimation()
    //{
    //    float totalWidth = (cards.Count - 1) * (cardWidth + cardSpacing);

    //    Vector3[] targetPositions = new Vector3[cards.Count];
    //    for (int i = 0; i < cards.Count; i++)
    //    {
    //        targetPositions[i] = TargetPositionX(i);
    //    }

    //    float elapsedTime = 0f;
    //    Vector3[] startPositions = cards.Select(card => card.transform.position).ToArray();

    //    while (elapsedTime < moveDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float t = elapsedTime / moveDuration;

    //        for (int i = 0; i < cards.Count; i++)
    //        {
    //            cards[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
    //        }
    //        yield return null;
    //    }

    //    // Đảm bảo vị trí cuối cùng chính xác
    //    for (int i = 0; i < cards.Count; i++)
    //    {
    //        cards[i].transform.position = targetPositions[i];
    //    }
    //}

    public IEnumerator DiscardCoroutine(Card choosing_cards)
    {
       
        UICard card = cards.FirstOrDefault(card => card.card == choosing_cards);
        int index = cards.IndexOf(card);
        cards.Remove(card);
        Destroy(card.gameObject);
        yield return new WaitForSeconds(0.2f);
        UpdateCard();
    }
    private void UpdateCard()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Vector3 target = new Vector3(TargetPositionX(i), cards[i].transform.position.y, 0);
            cards[i].Move(target);   
        }
    }
}
