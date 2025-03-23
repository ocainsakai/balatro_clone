using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayingManager : SingletonAbs<PlayingManager>
{
    [SerializeField] UIHandZone zone;
    private PokerHandEvaluator evaluator;
    [SerializeField] public List<Card> defaulsCards 
        => Resources.LoadAll<Card>("CardSO").ToList();


    public List<Card> playingCards;
    public List<Card> inDeckCards;
    public List<Card> inDiscardPile;
    public List<Card> inHandCards;
    public List<Card> inSelectCards;

    public int hand_size = 8;

    private Queue<Card> addQueue = new Queue<Card>();
    private Queue<Card> discardQueue = new Queue<Card>();
    private bool isProcessingQueue = false;
    public void Start()
    {
        playingCards = new List<Card>(defaulsCards);
        evaluator = new PokerHandEvaluator();
        Debug.Log(playingCards.Count);
        Initlize();
    }
    public void Initlize()
    {
        inDeckCards = new List<Card>(playingCards);
        inHandCards = new List<Card>();
        inDiscardPile = new List<Card>();
        inSelectCards = new List<Card>();
        Shuffe();
        DrawHand();
    }
    public void Shuffe()
    {
        System.Random rng = new System.Random();
        int n = inDeckCards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card card = inDeckCards[k];
            inDeckCards[k] = inDeckCards[n];
            inDeckCards[n] = card;
        }
    }
    public bool Select(Card card)
    {
        if (inSelectCards.Count >= 5) return false;
        inSelectCards.Add(card); return true;
    }
    public void UnSelect(Card card)
    {
        inSelectCards.Remove(card);
    }
    public void PlayHand()
    {
        // cal score
        Poker poker;
        List<Card> inPoker;
        int score = evaluator.CalculateHand(inSelectCards,out poker,out inPoker);
        Debug.Log(poker.name);
        Debug.Log(score);

        Discard();
        // check win
    }
    public void Discard()
    {
        // discard to Pile
        while (inSelectCards.Count > 0)
        {
            Card card = inSelectCards[0];
            inDiscardPile.Add(card);
            inSelectCards.Remove(card);
            inHandCards.Remove(card);
            discardQueue.Enqueue(card);
        }
        if (!isProcessingQueue)
        {
            StartCoroutine(DiscardProcessQueue());
        }
    }
    public void Sort(int kind)
    {
        StartCoroutine(zone.Sort(kind));
    }
    public void DrawHand()
    {
        for (int i = inHandCards.Count; i < hand_size; i++)
        {
            Card card = inDeckCards[0];
            inHandCards.Add(card);
            inDeckCards.Remove(card);
            addQueue.Enqueue(card);
        }
        if (!isProcessingQueue)
        {
            StartCoroutine(AddProcessQueue());
        }
    }
    //IEnumerator ProcessQueue()
    //{
    //    isProcessingQueue = true;
    //    while (queues.Count > 0) {
    //        Queue<Card> currentQueue = queues.Dequeue();
    //        if (currentQueue == addQueue)
    //        {
    //            while (currentQueue.Count > 0)
    //            {
    //                Card card = currentQueue.Dequeue();
    //                yield return StartCoroutine(zone.AddCardCoroutine(card));
    //                yield return new WaitForSeconds(0.2f);
    //            }
    //        }
    //        else if (currentQueue == discardQueue)
    //        {
    //            while (currentQueue.Count > 0)
    //            {
    //                Card card = currentQueue.Dequeue();
    //                yield return StartCoroutine(zone.DiscardCoroutine(card));
    //                yield return new WaitForSeconds(0.2f);
    //            }
    //        }
    //    }
    //    isProcessingQueue = false;
    ////}
    IEnumerator AddProcessQueue()
    {
        isProcessingQueue = true;
        while (addQueue.Count > 0)
        {
            Card card = addQueue.Dequeue();
            yield return StartCoroutine(zone.AddCardCoroutine(card));
            yield return new WaitForSeconds(0.05f); 
        }
        isProcessingQueue = false;
    }
    IEnumerator DiscardProcessQueue()
    {
        isProcessingQueue = true;
        while (discardQueue.Count > 0)
        {
            Card card = discardQueue.Dequeue();
            yield return StartCoroutine(zone.DiscardCoroutine(card));
            yield return new WaitForSeconds(0.05f);
        }
        isProcessingQueue = false;
        // zone sort
        yield return zone.Sort();
        DrawHand();
    }
}
