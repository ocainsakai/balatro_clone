using System;
using System.Collections.Generic;
using System.Linq;

using Debug = UnityEngine.Debug;



public class PokerHandEvaluator
{
    public int flushSize = 5;
    public int straightSize = 5;
    public int straightGap = 1;
    List<CardSO> inPoker = new List<CardSO>();
    public int CalculateHand(List<CardSO> hand, out Poker poker, out List<CardSO> inPoker)
    {
        this.inPoker.Clear();
        EvaluateHand(hand, out poker);

        foreach (CardSO card in this.inPoker)
        {
            poker.chips += card.baseValue;

        }
        int score = (int)(poker.chips * poker.multiple);
        inPoker = this.inPoker;
        return score;
    }
    public void EvaluateHand(List<CardSO> inSelect, out Poker poker)
    {
        
        if (IsRoyalStraightFlush(inSelect))
        {
            poker = Poker.straightFlush;
            //inPoker = hand;
            return;
        }
        if (IsStraightFlush(inSelect))
        {
            poker = Poker.straightFlush;
            //inPoker = hand;
            return;
        }
        if (IsFullHouse(inSelect))
        {
            poker = Poker.fullHouse;
            return;

        }     // Full House
        if (IsRoyalStraight(inSelect))
        {
            poker = Poker.straight;
            return;
        }
        if (IsFlush(inSelect))
        {
            poker = Poker.flush;
            return;

        }         // Flush
        if (IsStraight(inSelect))
        {
            poker = Poker.straight;
            return;

        }      // Straight 
        if (IsFourOfAKind(inSelect))
        {

            poker = Poker.fourOfAKind;
            //inPoker = hand.;
            return;

        }   // Four of a Kind
        if (IsThreeOfAKind(inSelect))
        {
            poker = Poker.threeOfAKind;
            return;

        } // Three of a Kind
        if (IsTwoPair(inSelect))
        {
            poker = Poker.twoPair;
            return;

        }      // Two Pair
        if (IsPair(inSelect))
        {
            poker = Poker.pair;
            return;

        }          // One Pair
        CardSO highCard = inSelect.OrderBy(card => card.rank).Last();
        inPoker = new List<CardSO> { highCard };
        poker = Poker.highCard;


    }

    public bool IsFlush(List<CardSO> hand)
    {
        if (hand.GroupBy(card => card.suit).All(g => g.Count() >= flushSize))
        {
            inPoker = new List<CardSO> (hand);

            return true;
        }
        return false;
    }
    public bool IsRoyalStraight(List<CardSO> hand)
    {
        if (IsStraight(hand) && hand.Any(card => card.IsRoyal()))
        {
            inPoker = new List<CardSO>(hand);


            return true;
        }
        return false ;
    }
    public bool IsStraight(List<CardSO> hand)
    {
        if (hand.Count <  straightSize) return false;
        var values = hand.Select(card => card.rank).OrderBy(v => v).ToList();
        for (int i = 0; i < values.Count - 1; i++)
        {
            int diff = values[i+1] - values[i];
            if(diff > straightGap || diff == 0) return false;
        }

        inPoker = new List<CardSO> (hand);
        return true;
    }

    public bool IsStraightFlush(List<CardSO> hand)
    {
        int size = Math.Min(flushSize, straightSize);
        return IsFlush(hand) && IsStraight(hand);
    }
    public bool IsRoyalStraightFlush(List<CardSO> hand)
    {
        int size = Math.Min(flushSize, straightSize);
        return IsFlush(hand) && IsRoyalStraight(hand);
    }

    public bool IsFourOfAKind(List<CardSO> hand)
    {
        if (hand.GroupBy(card => card.rank).All(g => g.Count() == 4))
        {
            inPoker = hand.GroupBy(card => card.rank).First(g => g.Count() == 4).ToList();
            return true;
        }
        return false;
    }

    public bool IsFullHouse(List<CardSO> hand)
    {
        
        var groups = hand.GroupBy(card => card.rank).Select(g => g.Count()).OrderByDescending(c => c).ToList();
        if (groups.Count >= 2 && groups[0] == 3 && groups[1] == 2)
        {
            inPoker = new List<CardSO>(hand);
            return true;
        }
        return false;
    }

    public bool IsThreeOfAKind(List<CardSO> hand)
    {
        if (hand.GroupBy(card => card.rank).All(g => g.Count() == 3))
        {
            inPoker = hand.GroupBy(card => card.rank).First(g => g.Count() == 3).ToList();
            return true;
        }
        return false;
    }

    public bool IsTwoPair(List<CardSO> hand)
    {
        if (hand.GroupBy(card => card.rank).Count(g => g.Count() == 2) == 2 )
        {
            inPoker = hand.GroupBy(card => card.rank).Where(g => g.Count() == 2).SelectMany(g => g).ToList();
            return true;
        }
        return false;
    }

    public bool IsPair(List<CardSO> hand)
    {
        if (hand.GroupBy(card => card.rank).Count(g => g.Count() == 2) == 1)
        {
            inPoker = hand.GroupBy(card => card.rank).Where(g => g.Count() == 2).SelectMany(g => g).ToList();
            return true;
        }
        return false;
    }
}

public struct Poker
{
    public string name;
    public int chips;
    public int multiple;
    public static Poker none = new Poker() { chips = 0 , multiple =0, name = ""}; 
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