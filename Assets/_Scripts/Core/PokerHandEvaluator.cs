using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

public static class PokerHandEvaluator
{
    public static IEnumerator CalculateHand(List<Card> hand, Action<int> onComplete)
    {
        Poker poker = new Poker();
        PokerHandEvaluator.EvaluateHand(hand, out poker);

        foreach (Card card in hand)
        {
            poker.chips += card.baseValue;
            GameManager.instance.UpdatePokerUI(poker);
            yield return new WaitForSeconds(0.2f);
        }

        int score = (int)(poker.chips * poker.multiple);
        onComplete?.Invoke(score);
    }
    public static void EvaluateHand(List<Card> hand, out Poker poker)
    {
        if (IsStraightFlush(hand)) {

            poker = Poker.straightFlush;
            return;
        }
        if (IsFourOfAKind(hand))
        {

            poker = Poker.fourOfAKind;
            return;

        }   // Four of a Kind
        if (IsFullHouse(hand))
        {
            poker = Poker.fullHouse;
            return;

        }     // Full House
        if (IsFlush(hand))
        {
            poker = Poker.flush;
            return;

        }         // Flush
        if (IsStraight(hand))
        {
            poker = Poker.straight;
            return;

        }      // Straight
        if (IsThreeOfAKind(hand))
        {
            poker = Poker.threeOfAKind;
            return;

        } // Three of a Kind
        if (IsTwoPair(hand))
        {
            poker = Poker.twoPair;
            return;

        }      // Two Pair
        if (IsPair(hand))
        {
            poker = Poker.pair;
            return;

        }          // One Pair

        poker = Poker.highCard;


    }

    public static bool IsFlush(List<Card> hand)
    {
        if (hand.Count != 5) return false;
        return hand.All(card => card.suit == hand[0].suit);
    }

    public static bool IsStraight(List<Card> hand)
    {
        if (hand.Count != 5) return false;

        var values = hand.Select(card => card.rank).OrderBy(v => v).ToList();
        return values.Zip(values.Skip(1), (a, b) => b - a).All(diff => diff == 1);
    }

    public static bool IsStraightFlush(List<Card> hand)
    {
        if (hand.Count != 5) return false;

        return IsFlush(hand) && IsStraight(hand);
    }

    public static bool IsFourOfAKind(List<Card> hand)
    {
        if (hand.Count < 4) return false;

        return hand.GroupBy(card => card.rank).Any(g => g.Count() == 4);
    }

    public static bool IsFullHouse(List<Card> hand)
    {
        if (hand.Count != 5) return false;

        var groups = hand.GroupBy(card => card.rank).Select(g => g.Count()).OrderByDescending(c => c).ToList();
        return groups.Count >= 2 && groups[0] == 3 && groups[1] == 2;
    }

    public static bool IsThreeOfAKind(List<Card> hand)
    {
        if (hand.Count < 3) return false;

        return hand.GroupBy(card => card.rank).Any(g => g.Count() == 3);
    }

    public static bool IsTwoPair(List<Card> hand)
    {
        if (hand.Count < 4) return false;

        return hand.GroupBy(card => card.rank).Count(g => g.Count() == 2) == 2;
    }

    public static bool IsPair(List<Card> hand)
    {
        if (hand.Count < 2) return false;

        return hand.GroupBy(card => card.rank).Any(g => g.Count() == 2);
    }
}

public struct Poker
{
    public string name;
    public int chips;
    public int multiple;

    public static Poker highCard = new Poker()
    { chips = 5, multiple = 1, name = "High Card" };

    public static Poker pair = new Poker()
    { chips = 10, multiple = 2, name = "Pair" };
            
    public static Poker twoPair = new Poker()
    { chips = 20, multiple = 2, name = "Two Pair" };

    public static Poker threeOfAKind = new Poker()
    { chips = 30, multiple = 3, name = "Three Of Kind" };

    public static Poker fourOfAKind = new Poker()
    { chips = 60, multiple = 7, name = "Four Of A Kind" };

    public static Poker fullHouse = new Poker()
    { chips = 40, multiple = 4, name = "Full House" };

    public static Poker straight = new Poker()
    { chips = 30, multiple = 4, name = "Straight" };

    public static Poker flush = new Poker()
    { chips = 35, multiple = 4, name = "Flush" };

    public static Poker straightFlush = new Poker()
    { chips = 100, multiple = 8, name = "Straight Flush" }; 

}