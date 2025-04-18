using TMPro;
using UnityEngine;
using Zenject;

namespace Game.CardSystem
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cardImage;
        [SerializeField] private TextMeshPro nameTxt;
        [SerializeField] private TextMeshPro descriptionTxt;

        private ICard card;

        [Inject]
        public void Construct(ICard card)
        {
            this.card = card;
        }

        private void UpdateVisual()
        {
            nameTxt.text = card.Name;
            descriptionTxt.text = card.Description;
            cardImage.sprite = card.Image;
        }
        // void animated
        // void onclick
        public void OnClick()
        {

        }

    }

}
