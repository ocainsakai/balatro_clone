using UnityEngine;

public class CardModel
{
    public readonly CardData cardData;
    public readonly CardView cardView;
    private ICardCollection cardCollection;
    public bool isSelected;
    public CardModel(CardData cardData, CardView cardView, ICardCollection cardCollection)
    {
        this.cardData = cardData;
        this.cardView = cardView;
        this.cardCollection = cardCollection;
        cardView.Init(cardData._image);
        cardView.OnClicked += CardView_OnClicked;
    }

    private void CardView_OnClicked()
    {
        if (isSelected)
        {
            isSelected = false;
            cardView.DeselectCard();
        }
        else if (cardCollection.CanSelect())
        {
            isSelected = true;
            cardView.SelectCard();
        }
    }
}
