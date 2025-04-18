using UnityEngine;

namespace Game.CardSystem
{
    public interface ICard
    {
        string CardId { get; }
        string Name { get; }
        string Description { get; }
        Sprite Image { get; }
    }
}