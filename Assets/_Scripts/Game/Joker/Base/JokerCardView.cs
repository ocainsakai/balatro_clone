using UnityEngine;

namespace Game.Jokers
{
    public class JokerCardView : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        public void SetData(JokerCard data)
        {
            spriteRenderer.sprite = data.Data.Artwork;
        }
    }
}