using UnityEngine;
namespace Balatro.Card {
    public interface ICard
    {
        string Name { get; }
        Sprite Artwork { get; }
        int Rank { get; }
        Suit Suit { get; }
        int Value => Rank == 14 ? Rank >= 10 ? 11 : 10 : Rank;
    }

    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }
}


