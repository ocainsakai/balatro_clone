using DG.Tweening;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Card
{
    public class StandardCard : MonoBehaviour, IStandardCard
    {

        [SerializeField] CardView cardView;
        [SerializeField] TextMeshPro text;
        [SerializeField] CardContainer cardContainer;
        public IStandardCard data { get; private set; }
        public string Suit => data.Suit;

        public int Rank => data.Rank;

        public string Name => data.Name;

        public Sprite sprite => data.sprite;

        public int Value => (Rank > 10) ? (Rank == 14) ? 11 : 10 : Rank;

        public bool isSelected;

        public void SetInit(IStandardCard card, CardContainer container)
        {
            this.data = card;
            text.gameObject.SetActive(false);
            cardView.RenderArt(card);
            cardView.OnClicked += OnClickedHandler;
            cardContainer = container;
        }
        public void OnCalculate()
        {
            text.gameObject.SetActive(true);
            text.text = $"+{data.Value}";
            var target = text.transform.position.y + 1f;
            text.transform.DOMoveY(target, 0.3f);
        }
        private void OnClickedHandler()
        {
            if (cardContainer.SelectedCount < 5 && !isSelected)
            {
                OnSelect();
            } else if (isSelected) 
            {
                OnUnselect();
            }
        }
        public void OnSelect()
        {
            isSelected = true;
            cardView.OnSelect();
            cardContainer.UpdatePokerHand();
        }
        public void OnUnselect()
        {
            isSelected = false;
            cardView.OnUnselect();
            cardContainer.UpdatePokerHand();
        }
        public void OnRemove()
        {
            transform.DOScale(0f, 0.3f).OnComplete(() => Destroy(this.gameObject));

        }
    }
}

