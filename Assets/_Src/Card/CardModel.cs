using UnityEngine;
using UniRx;
public class CardModel
{
    public readonly CardData cardData;
    public ICardCollection cardCollection;

    public bool IsInCombo = false;
    public ReactiveProperty<bool> IsSelected { get; private set; } = new ReactiveProperty<bool>(false);

    public CardModel(CardData cardData, ICardCollection cardCollection)
    {
        this.cardData = cardData;
        this.cardCollection = cardCollection;
        ResetState();
    }
    public void ResetState()
    {
        IsSelected.Value = (false);
        IsInCombo = false;
    }
    public void CardView_OnClicked()
    {
        Debug.Log(IsSelected.Value);
        if (IsSelected.Value)
        { 
            ResetState();
        }
        else if (cardCollection.CanSelect())
        {
            IsSelected.Value = (true);
        }
        
    }
}
