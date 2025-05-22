using UnityEngine;
using UniRx;
//[RequireComponent (typeof(SpriteRenderer))]
public class Card : MonoBehaviour
{
    public static bool CanSelect = true;
    public CardData Data;
    private CardView cardView;
    public bool IsFront;
    private ReactiveProperty<CardState> state = new ReactiveProperty<CardState>();
    public IReadOnlyReactiveProperty<CardState> State => state;
    public enum CardState
    {
        None,
        Hold,
        Select,
        Play,
        Score
    }
    private void Awake()
    {
        cardView = GetComponent<CardView>();
    }
    public void ChangeState(CardState state)
    {
        this.state.Value = state;
    }
    public void SetData(CardData cardData, Sprite cardBack)
    {
        Data = cardData;
        cardView.SetView(Data.Art, cardBack);
        cardView.OnClicked += OnClickHandle;
        ResetCard();
    }
    public void ResetCard()
    {
        state.Value = CardState.Hold;
    }
    private void OnClickHandle()
    {
        if (state.Value == CardState.Select)
        {
            state.Value = CardState.Hold;
            _ = cardView.LocolmotionY();
        } else if (CanSelect && state.Value == CardState.Hold) 
        {
            state.Value = CardState.Select;
            _ = cardView.LocolmotionY(0.5f);
        }
    }
    public void Flip(bool isFlip = true)
    {
        if (isFlip)
        {
            IsFront = !IsFront;
        }
        _ = cardView.Flip(IsFront);
    }

}
