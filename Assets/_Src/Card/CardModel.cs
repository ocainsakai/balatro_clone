using UnityEngine;
using UniRx;
public class CardModel
{
    public readonly CardData cardData;
    public ICardCollection cardCollection;

    public ReactiveProperty<bool> IsSelected { get; private set; }

    public CardModel(CardData cardData, ICardCollection cardCollection)
    {
        this.cardData = cardData;
        this.cardCollection = cardCollection;
        ResetState();
    }
    public void ResetState()
    {
        IsSelected = new ReactiveProperty<bool>(false);

    }
    public void CardView_OnClicked()
    {
        if (IsSelected.Value)
        {
            IsSelected.Value = (false);
        }
        else if (cardCollection.CanSelect())
        {
            IsSelected.Value = (true);
        }
        
    }
}
