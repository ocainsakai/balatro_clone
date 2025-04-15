using UnityEngine;
namespace Balatro.Cards.CardsRuntime
{
    public class Card : MonoBehaviour
    {
        public CardView CardView => GetComponentInChildren<CardView>();
        public CardData data;
        public bool isPlayed;
        public bool isCombo;

        public void Setup(CardData data)
        {
            this.data = data;
            CardView.Setup(data.Art, GetValue(data));
        }
        public int GetValue(CardData data)
        {
            return (data.Rank > 10) ? (data.Rank == 14) ? 11 : 10 : data.Rank;
        }
    }
}

