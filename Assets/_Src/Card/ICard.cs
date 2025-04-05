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
        string Suit {  get; }
        int Rank { get; }
        int Value { get; }
    }
    
}

