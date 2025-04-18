using UnityEngine;
using Zenject;

namespace Game.CardSystem
{
    public class Card : ICard
    {
        public string CardId => data.CardId;

        public string Name => data.Name;

        public string Description => data.Description;

        public Sprite Image => data.Image;
        public readonly ICard data;
        [Inject]
        public Card(ICard data)
        {
            this.data = data;
        }
    }

}

