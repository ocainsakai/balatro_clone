using System;
using Game.Jokers;
using UnityEditor;
using UnityEngine;

namespace Game.Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
    public class CardData : ScriptableObject
    {
        public SerializableGuid CardDataId;
        public string Name;
        public Sprite Artwork;
        public string Description;
        public CardRank Rank;
        public CardSuit Suit;
        public int Value => (int) Rank == 14 ? 11 : (int) Rank > 10 ? 10 : (int)Rank;

        //[SerializeReference]
        //public IEffect Effect;

#if UNITY_EDITOR
        public void RegenerateGuid()
        {
            CardDataId = SerializableGuid.NewGuid();
            EditorUtility.SetDirty(this); // Mark as changed so Unity will save
        }
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

                    var sprites = Resources.Load<Sprite>($"Art/PNG/card-{Suit.ToString()}-{i}");
                    Artwork = sprites;

                }
                Name = (Rank) + " Of " + (Suit);
                //Effect = new EffectPlusChip() {Chip = Value};
            }
        }
#endif

    }
    public enum CardSuit
    {
        Hearts = 0,
        Diamonds = 1,
        Clubs = 2,
        Spades = 3,
    }
    public enum CardRank
    {
        LowAce = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

}