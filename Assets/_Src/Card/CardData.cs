using System;
using UnityEngine;

namespace Balatro.Cards
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Card Data")]
    public class CardData : ScriptableObject
    {
        public string Name;
        public Sprite Art;
        public CardSuit Suit;
        public int Rank;

        private void OnValidate()
        {
            if (this.name != null)
            {
                var token = this.name.Split(" ");
                if (token.Length > 1)
                {
                    this.Name = this.name;
                    Suit = Enum.TryParse<CardSuit>(token[0], out CardSuit suit) ? suit : CardSuit.Hearts;
                    Rank = int.Parse(token[1]);
                    Art = Resources.Load<Sprite>($"PNG/card-{token[0]}-{token[1]}");
                }
            }
        }
    }

    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

}
