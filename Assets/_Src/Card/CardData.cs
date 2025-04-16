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
                    var _rank = int.Parse(token[1]);
                    Rank = _rank  == 1 ? 14 : _rank;
                    Art = Resources.Load<Sprite>($"Art/PNG/card-{token[0]}-{token[1]}");
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
