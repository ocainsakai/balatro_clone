using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game.Cards
{
    public static class CardsSorter
    {
        public enum SortType
        {
            ByRank,
            BySuit
        }
        public static SortType currentType;
        public static IEnumerable<Card> SortByRank(IEnumerable<Card> cards)
        {
            return cards.OrderByDescending(x => x.Data.Rank).ThenByDescending(x => x.Data.Suit);
        }
        public static IEnumerable<Card> SortBySuit(IEnumerable<Card> cards)
        {
            return cards.OrderByDescending(x => x.Data.Suit).ThenByDescending(x => x.Data.Rank);
        }
    }
    
}