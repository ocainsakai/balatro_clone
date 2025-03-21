using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> default_cards;
    public List<Card> playing_cards;
    public List<Card> choosing_cards;
    public List<Card> hand_cards;
    public List<Card> discard_pile;
    public List<Card> played_cards;

    public int hand_size = 8;
    public int hand_count => hand_cards.Count;


    void TestPoker()
    {
        Card card1heart = new Card() { suit = Card.CardSuit.Hearts, rank = Card.CardRank.Ace};
        Card card2heart = new Card() { suit = Card.CardSuit.Hearts, rank = Card.CardRank.Two};
        Card card3heart = new Card() { suit = Card.CardSuit.Hearts, rank = Card.CardRank.Three};
        Card card4heart = new Card() { suit = Card.CardSuit.Hearts, rank = Card.CardRank.Four};
        Card card5heart = new Card() { suit = Card.CardSuit.Hearts, rank = Card.CardRank.Five};
        Card card6heart = new Card() { suit = Card.CardSuit.Hearts, rank = Card.CardRank.Six};
        Card card1club = new Card() { suit = Card.CardSuit.Clubs, rank = Card.CardRank.Ace};
        Card card1diamond = new Card() { suit = Card.CardSuit.Diamonds, rank = Card.CardRank.Ace};
        Card card1spade = new Card() { suit = Card.CardSuit.Spades, rank = Card.CardRank.Ace};
        Card card2club = new Card() { suit = Card.CardSuit.Clubs, rank = Card.CardRank.Two};

        bool result;
        result = PokerHandEvaluator.IsPair(new List<Card>() { card1club,card1diamond,card2club});
        Debug.Log("pair " + result);
        result = PokerHandEvaluator.IsTwoPair(new List<Card>() { card1club, card1diamond, card2club , card2heart});
        Debug.Log("IsTwoPair " + result);
        result = PokerHandEvaluator.IsFlush(new List<Card>() { card1heart, card2heart, card3heart, card4heart, card5heart });
        Debug.Log("IsFlush " + result);
        result = PokerHandEvaluator.IsFourOfAKind(new List<Card>() { card1club, card1diamond, card1heart, card1spade });
        Debug.Log("IsFourOfAKind " + result);
        result = PokerHandEvaluator.IsFullHouse(new List<Card>() { card1club, card1diamond, card2club, card2heart, card1spade });
        Debug.Log("IsFullHouse " + result);
        result = PokerHandEvaluator.IsStraight(new List<Card>() { card1heart, card2heart, card3heart, card4heart, card5heart });
        Debug.Log("IsStraight " + result);
        result = PokerHandEvaluator.IsStraightFlush(new List<Card>() { card1heart, card2heart, card3heart, card4heart, card5heart });
        Debug.Log("IsStraightFlush " + result);

    }
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
        played_cards.AddRange(choosing_cards);
        playing_cards.AddRange(discard_pile);
        choosing_cards.Clear();
        discard_pile.Clear();
    }
    public IEnumerator Discard(Card card, Action<Card> onComplete)
    {
        if (hand_cards.Contains(card))
        {
            discard_pile.Add(card);
            hand_cards.Remove(card);
            yield return new WaitForSeconds(0.2f);
            onComplete?.Invoke(card);
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
}
