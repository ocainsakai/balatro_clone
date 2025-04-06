using DG.Tweening;
using TMPro;
using UnityEngine;


namespace Card
{
    public class StandardCard : MonoBehaviour, IStandardCard
    {

        [SerializeField] CardView cardView;
        [SerializeField] TextMeshPro text;
        [SerializeField] CardManager manager;
        public IStandardCard data { get; private set; }
        public Suit Suit => data.Suit;

        public int Rank => data.Rank;

        public string Name => data.Name;

        public Sprite sprite => data.sprite;

        public int Value => (Rank > 10) ? (Rank == 14) ? 11 : 10 : Rank;

        public bool isSelected;

        public void SetInit(IStandardCard card, CardManager manager)
        {
            this.data = card;
            text.gameObject.SetActive(false);
            cardView.RenderArt(card);
            cardView.OnClicked += OnClickedHandler;
            this.manager = manager;
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
            if (manager.SelectedCount < 5 && !isSelected)
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
            manager.UpdatePokerHand();
        }
        public void OnUnselect()
        {
            isSelected = false;
            cardView.OnUnselect();
            manager.UpdatePokerHand();
        }
        public void OnRemove()
        {
            transform.DOScale(0f, 0.3f).OnComplete(() => Destroy(this.gameObject));

        }
    }
}

