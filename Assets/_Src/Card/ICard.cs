using UnityEngine;

namespace Card
{
    public interface ICard
    {
        string Name { get; }
        Sprite sprite { get; }
    }
    public interface IStandardCard : ICard
    {
        Suit Suit {  get; }
        int Rank { get; }
        int Value { get; }
    }
    public enum Suit {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

}

