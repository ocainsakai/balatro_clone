using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Transform cardPrf => Resources.Load<Transform>("_Prefabs/CardPrf");
    public List<CardSO> deck = new List<CardSO>();
    public List<CardSO> hand = new List<CardSO>();
    public List<Card> choosing = new List<Card>();
    private Transform handArea;
    public List<CardSO> initDeck => Resources.Load<CardListSO>(nameof(CardListSO)).cards;
    public int handSize = 8;
    public int choosingMax = 5;

    public bool canChoose => (choosing.Count < choosingMax);
    public void OnEnable()
    {
        handArea = GameObject.FindGameObjectWithTag("Respawn").transform;
        deck = initDeck;
    }
    public void Choose(Card card)
    {
        if (choosing.Count < choosingMax)
        {
            choosing.Add(card);
        }
    }
    public void GiveUp(Card card)
    {
        if (choosing.Contains(card))
        {
            choosing.Remove(card);
        }
    }
    public void CreateCard(CardSO cardSO)
    {
        Transform newCard = Instantiate(cardPrf);
        newCard.SetParent(handArea.transform);
        newCard.gameObject.SetActive(true);
        
        
        Card newCtrl = newCard.GetComponent<Card>();
        newCtrl.cardSO = cardSO;
        newCtrl.SetImage();
        newCtrl.deckManager = this;


    }
    public void DrawCards()
    {
  
        for (int i = hand.Count; i < handSize; i++)
        {
            if (deck.Count > 0)
            {
                CardSO cardSO = deck[0];
                hand.Add(cardSO);
                deck.RemoveAt(0);
                CreateCard(cardSO);
            }
        }
    }

    public void Discard()
    {
        foreach(Card card in choosing)
        {
            if (card != null)
            {
                deck.Add(card.cardSO);
                hand.Remove(card.cardSO);
            }
        }
        CleanChosen();
        DrawCards();
    }
    private void CleanChosen()
    {
        foreach (Card card in choosing)
        {
            Destroy(card.gameObject);
        }
        choosing.Clear();
    }
    
    public int Calculate()
    {
        PokerHandType handType = PokerHandEvaluator.EvaluateHand(choosing);
        int totalScore = ScoreCalculator.CalculateScore(handType);
        Debug.Log($"HandType: {handType}. Total: {totalScore}");
        foreach (Card card in choosing)
        {
            if (card != null)
            {
                hand.Remove(card.cardSO);
            }
        }
        CleanChosen();
        DrawCards();
        return totalScore;
    }
    public void ShuffleDeck()
    {
        for (int i = 0;i < deck.Count; i++)
        {
            CardSO temp = deck[i];
            int randomIndex = Random.Range(i,deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
    public void HideHand()
    {
        handArea.gameObject.SetActive(false);
    }
    public void ShowHand()
    {
        handArea.gameObject.SetActive(false);
    }
}
