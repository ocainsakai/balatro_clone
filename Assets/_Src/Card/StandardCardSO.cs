using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName = "SO/StandardCardSO")]
    public class StandardCardSO : ScriptableObject, IStandardCard
    {
        public IStandardCard data => this;
        public string Name => _name;
        public Sprite sprite => _sprite;
        public int Rank => _rank;
        public string Suit => _suit;

        public int Value => (Rank > 10) ? (Rank == 14) ? 11 : 10 : Rank;

        public string _name;
        public Sprite _sprite;
        public int _rank;
        public string _suit;
        private void OnValidate()   
        {
            if (!string.IsNullOrEmpty(this.name))
            {
                string[] parts = this.name.Split(' ');

                if (parts.Length == 2)
                {
                    _suit = parts[0]; 
                    if (int.TryParse(parts[1], out int parsedRank))
                    {
                        if(parsedRank == 1)
                        {
                            _rank = 14;
                        } else 
                            _rank = parsedRank; 
                        _sprite = Resources.Load<Sprite>($"PNG/card-{_suit}-{parsedRank}");
                    }
                }
            }
        }

        
    }
}
