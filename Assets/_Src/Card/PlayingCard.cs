
using DG.Tweening;
using UnityEngine;

namespace Balatro.Card
{
    public class PlayingCard : MonoBehaviour, ICard
    {
        [SerializeField] PlayingCardView _view;

        public CardManager Manager { get; private set; }
        public string Name { get; private set; }
        public Sprite Artwork { get; private set; }
        public int Rank { get; private set; }
        public Suit Suit { get; private set; }
        public bool IsSelected;
        public void Initialize(ICard card, CardManager manager)
        {
            if (card == null)
            {
                Debug.LogError("ICard provided to Initialize is null!");
                return;
            }
            Manager = manager;

            // Gán giá trị từ card
            Name = card.Name;
            Artwork = card.Artwork;
            Rank = card.Rank;
            Suit = card.Suit;

            var initY = GetComponent<RectTransform>().anchoredPosition.y;
            
            _view.SetInit(Artwork);
            _view.OnClick = OnClickedHandler;
        }
        private void OnClickedHandler()
        {
            if (Manager.SelectedCount < 5 && !IsSelected)
            {
                OnSelect();
            }
            else if (IsSelected)
            {
                OnUnselect();
            }
        }
        public void OnSelect()
        {
            IsSelected = true;
            _view.OnSelect();
            //manager.UpdatePokerHand();
        }
        public void OnUnselect()
        {
            IsSelected = false;
            _view.OnUnselect();
            //manager.UpdatePokerHand();
        }
        public void OnRemove()
        {
            transform.DOScale(0f, 0.3f).OnComplete(() => Destroy(this.gameObject));
        }

    }

}
