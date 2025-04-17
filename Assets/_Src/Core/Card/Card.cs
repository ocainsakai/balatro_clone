using Balatro.Cards.UI;
using System;
using UnityEngine;
namespace Balatro.Cards
{
    public class Card : MonoBehaviour
    {
        public CardView CardView => GetComponentInChildren<CardView>();
        public CardData data;
        public bool isPlayed;
        public bool isCombo;

        public void Setup(CardData data, Action<CardView> onCardClicked)
        {
            this.data = data;
            CardView.Setup(data.Art, GetValue(data));
            CardView.OnSelected += onCardClicked;
        }
        public int GetValue()
        {
            return GetValue(data);
        }
        public int GetValue(CardData data)
        {
            return (data.Rank > 10) ? (data.Rank == 14) ? 11 : 10 : data.Rank;
        }
        public void Remove()
        {
            Destroy(gameObject);
        }

    }
}

