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

        public static JokerCardView Create(JokerCard data, Transform parent)
        {
            var prefabs = Resources.Load<JokerCardView>("_Prefabs/JokerPrefab");
            var jokerView = Instantiate(prefabs, parent);
            jokerView.SetData(data);
            return jokerView;

        }
    }
}