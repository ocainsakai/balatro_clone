using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : AbstractSingleton<DeckManager>
{

    public event Action<Card> OnDiscardCard;
    public event Action<Card> OnDrawCard;
    public event Action<int> OnSort;


    public List<Card> default_cards;
    public List<Card> playing_cards;
    public List<Card> choosing_cards;
    public List<Card> hand_cards;
    public List<Card> discard_pile;
    public List<Card> played_cards;

    public int hand_size = 8;
    public int hand_count => hand_cards.Count;
    public bool canChoose => choosing_cards.Count < 5;
    
    public void Initialize()
    {
        playing_cards.AddRange(default_cards);
        Shuffe();
    } 
    public void End()
    {
        playing_cards.AddRange(played_cards);
        playing_cards.AddRange(hand_cards);
        played_cards.Clear();
        hand_cards.Clear();
    }
    public void Shuffe()
    {
        System.Random rng = new System.Random();
        int n = playing_cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card card = playing_cards[k];
            playing_cards[k] = playing_cards[n];
            playing_cards[n] = card;
        }
    }
    public void PlayHand()
    {
        if (GameManager.instance.run.play_hands <= 0 || choosing_cards.Count <= 0) return;
        StartCoroutine(PlayHandCoroutine());
    }
    public void Discard()
    {
        if (GameManager.instance.run.discards <= 0 || choosing_cards.Count <= 0) return;

        StartCoroutine(DiscardHandCoroutine());
    }
    public void Choosing(Card card, bool isChoosing)
    {
        if (isChoosing)
        {
            choosing_cards.Add(card);
        }
        else
        {
            choosing_cards.Remove(card);
        }
        Poker poker;
        PokerHandEvaluator.EvaluateHand(choosing_cards, out poker);
        //OnPokerUpdate?.Invoke(poker);
        //    // poker hand and update ui

    }
    
    public void Sort(int index =0 )
    {
        OnSort?.Invoke(index);
    }
    
    private IEnumerator PlayHandCoroutine()
    {
        int score = 0;
        yield return (PokerHandEvaluator.CalculateHand(choosing_cards,result => score = result));
        yield return DiscardHand();
        yield return DrawHand();
    }
    private IEnumerator DiscardHandCoroutine()
    {
        yield return (DiscardHand());
        yield return (DrawHand());
    }
    public IEnumerator DiscardHand()
    {
        for (int i = choosing_cards.Count - 1; i >= 0; i--)
        {
            yield return StartCoroutine(DiscardCoroutine(choosing_cards[i], card => OnDiscardCard?.Invoke(card)));
        }
    }
    public IEnumerator DrawHand()
    {
        for (int i = hand_count; i < hand_size; i++)
        {
            yield return StartCoroutine(Draw(card => OnDrawCard?.Invoke(card)));
        }
    }
    public IEnumerator Draw(Action<Card> onComplete)
    {
        if(playing_cards == null)
        {
            playing_cards.AddRange(discard_pile);
            discard_pile.Clear();
        } else
        if (playing_cards != null)
        {
            Card card = playing_cards[0];
            playing_cards.Remove(card);
            hand_cards.Add(card);
            yield return new WaitForSeconds(0.2f);
            onComplete?.Invoke(card);
        }
    }
    public IEnumerator DiscardCoroutine(Card card, Action<Card> onComplete)
    {
        if (hand_cards.Contains(card))
        {
            discard_pile.Add(card);
            hand_cards.Remove(card);
            choosing_cards.Remove(card);
            yield return new WaitForSeconds(0.2f);
            Sort();
            onComplete?.Invoke(card);
        }
    }

}
