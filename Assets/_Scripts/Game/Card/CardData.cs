using UnityEngine;

namespace Game.Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
    public class CardData : ScriptableObject
    {
        public string Name;
        public Sprite Artwork;
        public string Description;
        public int Rank;
        public int Suit;

        void OnValidate()
        {
            if (!string.IsNullOrEmpty(this.name))
            {
                var token = name.Split('_');
                if (token.Length > 1)
                {
                    Suit = CardParser.ParseSuit(name);
                    Rank = CardParser.ParseRank(name);
                    int i = Rank == CardRank.Ace ? 1 : (int)Rank;

                    var sprites = Resources.Load<Sprite>($"Art/PNG/card-{CardParser.GetSuitName(Suit)}-{i}");
                    Artwork = sprites;

                }
                Name = CardParser.GetRankName(Rank) + " Of " + CardParser.GetSuitName(Suit);
            }
        }
    }
    public struct CardSuit
    {
        public const int Hearts = 0;
        public const int Diamonds = 1;
        public const int Clubs = 2;
        public const int Spades = 3;
    }
    public struct CardRank
    {
        public const int LowAce = 1;
        public const int Two = 2;
        public const int Three = 3;
        public const int Four = 4;
        public const int Five = 5;
        public const int Six = 6;
        public const int Seven = 7;
        public const int Eight = 8;
        public const int Nine = 9;
        public const int Ten = 10;
        public const int Jack = 11;
        public const int Queen = 12;
        public const int King = 13;
        public const int Ace = 14;
    }
}