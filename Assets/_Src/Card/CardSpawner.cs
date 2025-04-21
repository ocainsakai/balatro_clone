using UnityEngine;
using UniRx;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrf;
    [SerializeField] private Transform _cardContainer;

    private Dictionary<CardModel, CardView> _viewMap = new();

    public CardView SpawnCard(CardModel card, BaseCardCollection baseCardCollection)
    {
        CardView view = InstantiateCard(card);
        if (view == null) return null;
        InitializeCardView(view, card);
        SetupCardInteractions(view, card, baseCardCollection);

        _viewMap[card] = view;
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

    private void SetupCardInteractions(CardView view, CardModel card, BaseCardCollection baseCardCollection)
    {
 
        view.OnClicked.Subscribe(_ => card.CardView_OnClicked()).AddTo(view);

   
        card.IsSelected.Subscribe(isSelected =>
        {
            if (isSelected)
            {
                Debug.Log("is select");
                view.SelectCard();
            }
            else
            {
                view.DeselectCard();
            }
        }).AddTo(view);
        baseCardCollection.OnCardRemoved.Subscribe(removedCard =>
        {
            if (_viewMap.TryGetValue(removedCard, out var view))
            {
                view.DestroyCard();
                _viewMap.Remove(removedCard);
            }
        });
    }
}