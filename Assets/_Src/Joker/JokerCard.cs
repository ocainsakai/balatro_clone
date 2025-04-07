using TMPro;
using UnityEngine;

namespace Joker {
    public class JokerCard : MonoBehaviour, IJoker
    {
        [SerializeField]  SpriteRenderer spriteRenderer;
        [SerializeField] TextMeshPro text;

        public Rarity Rarity { get; set; } 

        public int Price {get; set;}

        public string Name {get; set;}

        public Sprite sprite { get; set;}
        public void SetJoker(IJoker joker)
        {
            this.Rarity = joker.Rarity;
            this.Name = joker.Name;
            this.sprite = joker.sprite;
            this.Price = joker.Price;
            spriteRenderer.sprite = sprite;
        }
    }
}


