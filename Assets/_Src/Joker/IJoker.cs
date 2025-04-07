using Card;
using UnityEngine;

namespace Joker
{
    public interface IJoker : ICard
    {
        int Cost { get; }
        Rarity Rarity { get; }

    }
    public enum Rarity
    {
        None = 0,
        Common,
        Uncommon,
        Rare,
        Legendary
    }
}

