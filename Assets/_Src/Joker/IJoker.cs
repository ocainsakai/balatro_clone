using Card;
using UnityEngine;

namespace Joker
{
    public interface IJoker : IItem
    {
        Rarity Rarity { get; }

    }
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }
}

