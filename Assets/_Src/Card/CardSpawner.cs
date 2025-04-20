using UnityEngine;
using UniRx;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrf;
    [SerializeField] private Transform _cardContainer;

    public CardView SpawnCard(CardModel card)
    {
        CardView view = InstantiateCard(card);
        if (view == null) return null;

        // Bước 2: Khởi tạo CardView
        InitializeCardView(view, card);

        // Bước 3: Gán sự kiện và subscription
        SetupCardInteractions(view, card);

        //// Bước 4: Sắp xếp lại
        //RepositionCards();

        return view;
    }

    private CardView InstantiateCard(CardModel card)
    {
        if (_cardPrf == null || _cardContainer == null)
        {
            Debug.LogError("Card prefab or container is not assigned!");
            return null;
        }

        var go = Instantiate(_cardPrf, _cardContainer);
        go.name = card.cardData?.name ?? "Unnamed Card";
        var view = go.GetComponent<CardView>();

        if (view == null)
        {
            Debug.LogError("CardView component not found on instantiated GameObject!");
            Destroy(go);
            return null;
        }

        return view;
    }

    private void InitializeCardView(CardView view, CardModel card)
    {
        if (card.cardData?._image == null)
        {
            Debug.LogWarning("Card image is null!");
        }
        view.Init(card.cardData?._image);
    }

    private void SetupCardInteractions(CardView view, CardModel card)
    {
        // Gán sự kiện click
        view.OnClicked.Subscribe(_ => card.CardView_OnClicked()).AddTo(view);

        // Subscribe IsSelected để điều khiển Select/Deselect
        card.IsSelected.Subscribe(isSelected =>
        {
            if (isSelected)
                view.SelectCard();
            else
                view.DeselectCard();
        }).AddTo(view);
    }

    //private void RepositionCards()
    //{
    //    // Sắp xếp lại nếu dùng HorizontalGridLayout
    //    _cardContainer.GetComponent<HorizontalGridLayout>()?.RepositionChildren();
    //}
}