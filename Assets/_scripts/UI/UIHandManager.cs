using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using BalatroClone.Cards;



public class UIHandManager : SingletonAbs<UIHandManager>
{
    [SerializeField] private GameObject uiCardPrf;
    [SerializeField] private Transform deckPosition;
    public Poker currentPoker;

    private PokerHandEvaluator evaluator;


    public List<Card> deck;
    public List<Card> hand;
    public List<Card> discard_pile;
    public List<CardSO> inSelectCards => hand.Where(card => card.isChoosing).Select(card => card.cardSO).ToList();
    public float cardWidth = 0.75f;
    public float cardSpace = 0.15f;
    public float effectDuration = 0.3f;

    public bool isSortByRank;
    public bool inSelecting;

    public int hand_size = 8;
    private void Start()
    {
        evaluator = new PokerHandEvaluator();
        isSortByRank = true;
    }
    Card CreateCard(CardSO card)
    {
        Card newCard = Instantiate(uiCardPrf).GetComponent<Card>();
        deck.Add(newCard);
        newCard.Initlize(card);
        newCard.OnSelect += SelectCard;
        //newCard.OnChoosingCard += PlayingManager.instance.Choosing;
        newCard.transform.SetParent(transform);
        newCard.transform.SetPositionAndRotation(deckPosition.position,Quaternion.identity);
        newCard.transform.localScale = Vector3.one;
        //newCard.transform.position = TargetPositionX(cards.Count);
        newCard.gameObject.SetActive(false);
        return newCard;
    }
    public void Initlize(List<CardSO> initDeck)
    {
        foreach (CardSO card in initDeck)
        {
            CreateCard(card);
        }
        Shuffe();
        DrawHand();
    }
    public void Shuffe()
    {
        System.Random rng = new System.Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card card = deck[k];
            deck[k] = deck[n];
            deck[n] = card;
        }
    }
    public void DrawHand()
    {
        if (deck.Count == 0)
        {
            deck.AddRange(discard_pile);
            discard_pile.Clear();
        }
        for (int i = hand.Count; i < hand_size; i++)
        {
            Card card = deck[0];
            card.gameObject.SetActive(true);
            hand.Add(card);
            deck.Remove(card);
        }
        Debug.Log(hand.Count);
        StartCoroutine(SortProcess());
        UIManager.instance.NonePoker();
    }
    public void SelectCard(Card card)
    {
        if (inSelectCards.Count >= 5 && !card.isChoosing) return;
        card.isChoosing = !card.isChoosing;
        float moveOffset = card.isChoosing ? 10 : -10;
        card.transform.DOMoveY(card.transform.position.y + moveOffset, 0.2f);
        ShowPoker();
    }
    private void ShowPoker()
    {
        if (inSelectCards.Count <= 0)
        {
            UIManager.instance.NonePoker();
            currentPoker = Poker.none;
            return;
        }

        Poker poker = new Poker();
        evaluator.EvaluateHand(inSelectCards, out poker);
        if (string.Compare(poker.name, currentPoker.name) != 0)
        {
            UIManager.instance.UpdatePokerHand(poker);
            currentPoker = poker;
        }
        //ui_poker_hand.UpdatePokerHand(Poker.none);  

    }
    
    public void PlayHand()
    {
        if (GameManager.instance.run.play_hands <= 0
            || inSelectCards.Count == 0) return;
        GameManager.instance.PlayHand();
        StartCoroutine(PlayProcess());
    }
    public void Discard()
    {
        if (GameManager.instance.run.discards <= 0
            || inSelectCards.Count == 0) return;
        GameManager.instance.Discard();
        //GameManager.instance.uiRun.UpdateRunUI();
        // discard to Pile

        StartCoroutine(DiscardProcess());
    }
    public void SortByRank()
    {
        StartCoroutine(SortProcess(2));
    }
    public void SortBySuit()
    {
        StartCoroutine(SortProcess(1));
    }

    public IEnumerator SortProcess(int kind = 0)
    {
        Debug.Log(hand.Count);
        if (kind != 0) isSortByRank = kind % 2 == 0;
        hand = isSortByRank ? Utilities.SortByRank(hand) : Utilities.SortBySuit(hand);
        for (int i = 0; i < hand.Count; i++)
        {
            Card card = hand[i];

            card.isChoosing = false;
   
            Vector3 start = card.transform.position;
            Vector3 target = Target(i);
            card.transform.DOMove(target, effectDuration).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(0.05f);

        }
        //yield return new WaitForSeconds(effectDuration);
    }
    public IEnumerator DiscardProcess()
    {
        foreach (Card uiCard in hand)
        {
            if (!uiCard.isChoosing) continue;
            discard_pile.Add(uiCard);
            uiCard.isChoosing = false;
            Vector3 start = uiCard.transform.position;
            Vector3 target = deckPosition.position;
            uiCard.transform.DOMove(target, effectDuration).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(0.05f);
            uiCard.gameObject.SetActive(false);
        }
        hand.RemoveAll(x => discard_pile.Contains(x));
        DrawHand();
    }
    public IEnumerator PlayProcess()
    {
        // cal anim
        Poker poker;
        List<CardSO> inPoker;
        int score = evaluator.CalculateHand(
            inSelectCards, out poker, out inPoker);
        foreach (Card card in hand)
        {
            if (!inPoker.Contains(card.cardSO)) continue;
            UIManager.instance.UpdateChip(card.cardSO.baseValue);
            yield return card.PlusCoroutine();
        }
        //yield return GameManager.instance.AddScore(score);
        // update run
        yield return DiscardProcess();
        DrawHand(); 
    }

    Vector3 Target(int index)
    {
        Vector3 startPoint = transform.position;
        return startPoint + Vector3.right * (cardWidth+cardSpace) * index; 
    }

}
