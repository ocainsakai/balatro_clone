using UnityEngine;

namespace Joker
{
    [CreateAssetMenu(fileName = "JokerSO", menuName = "Scriptable Objects/JokerSO")]
    public class JokerSO : ScriptableObject, IJoker
    {
        [SerializeField] string _name;
        [SerializeField] Sprite _sprite;
        [SerializeField] int _cost;
        [SerializeField] Rarity _rarity;
        public int Cost => _cost;

        public Rarity Rarity => _rarity;

        public string Name => _name;

        public Sprite sprite => _sprite;

        void OnValidate()
        {
            if (!string.IsNullOrEmpty(this.name))
            {
                this._name = this.name;
                _sprite = Resources.Load<Sprite>($"joker_images/{this.name}");
                
            }
        }
    }

}
