using Card;
using UnityEngine;

namespace Joker
{
    public interface IJoker : Item
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

