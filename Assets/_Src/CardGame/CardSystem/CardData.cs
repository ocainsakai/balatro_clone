using UnityEngine;

namespace Game.CardSystem
{
    [CreateAssetMenu(menuName = ("CardSystem/Card Data"))]
    public class CardData : ScriptableObject, ICard
    {
        [SerializeField] string _cardID;
        [SerializeField] string _cardName;
        [SerializeField] string _cardDescription;
        [SerializeField] Sprite _image;
        public string CardId => _cardID;    

        public string Name => _cardName;

        public string Description => _cardDescription;

        public Sprite Image => _image;
#if UNITY_EDITOR
        void OnValidate()
        {
            if (!string.IsNullOrEmpty(this.name))
            {
                _cardName = this.name;
                _cardID = this.GetHashCode().ToString();
                // load card art
            }
        }
#endif
    }

}

