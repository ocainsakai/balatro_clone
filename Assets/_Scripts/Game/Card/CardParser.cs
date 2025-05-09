using System;
using UnityEngine;

namespace Game.Cards
{
    public static class CardParser
    {
        public static CardRank ParseRank(string name)
        {
            foreach (var word in name.Split('_', StringSplitOptions.RemoveEmptyEntries))
            {
                if (Enum.TryParse<CardRank>(word, out var rank))
                    return rank;
            }

            Debug.LogWarning($"Cannot parse rank from '{name}', defaulting to CardRank.Two");
            return CardRank.Two;
        }

        public static CardSuit ParseSuit(string name)
        {
            foreach (var word in name.Split('_', StringSplitOptions.RemoveEmptyEntries))
            {
                if (Enum.TryParse<CardSuit>(word, out var suit))
                    return suit;
            }

            Debug.LogWarning($"Cannot parse suit from '{name}', defaulting to CardSuit.Hearts");
            return CardSuit.Hearts;
        }

        public static string GetSuitName(int suit)
        {
            if (Enum.IsDefined(typeof(CardSuit), suit))
                return ((CardSuit)suit).ToString();

            return "Unknown";
        }

        public static string GetRankName(int rank)
        {
            if (Enum.IsDefined(typeof(CardRank), rank))
                return ((CardRank)rank).ToString();

            return "Unknown";
        }
    }

}