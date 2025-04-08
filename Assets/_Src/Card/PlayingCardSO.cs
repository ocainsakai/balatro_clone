using System;
using UnityEngine;

namespace Balatro.Card
{
    [CreateAssetMenu(fileName = "PlayingCardSO", menuName = "Scriptable Objects/PlayingCardSO")]
    public class PlayingCardSO : ScriptableObject, ICard
    {
        [SerializeField] string _name;
        public string Name => _name;

        [SerializeField] Sprite _artwork;
        public Sprite Artwork => _artwork;
        [SerializeField] int _rank;
        public int Rank => _rank;
        [SerializeField] Suit _suit;
        public Suit Suit => _suit;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (!string.IsNullOrEmpty(this.name))
            {
                string[] parts = this.name.Split(' ');

                if (parts.Length == 2)
                {
                    _suit = ParseSuit(parts[0]);
                    if (int.TryParse(parts[1], out int parsedRank))
                    {
                        if (parsedRank == 1)
                        {
                            _rank = 14;
                        }
                        else
                            _rank = parsedRank;
                        _artwork = Resources.Load<Sprite>($"PNG/card-{_suit}-{parsedRank}");
                    }
                }
            }
        }
#endif
        public Suit ParseSuit(string input)
        {
            {
                return (Suit)Enum.Parse(typeof(Suit), input, true);
            }
        }
    } 

}
    

