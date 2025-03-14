using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerClickHandler
{
    public CardSO cardSO;

    private Run running => GameManager.Instance.running;
    [SerializeField] private Sprite cardArt;
    [SerializeField] private Sprite backArt => DeckManager.Instance.image.sprite;

    [SerializeField] private int moveOffset = 10;
    private bool isChoosing;
    
    public void SetCard(CardSO cardSO, bool isFlip = true)
    {
        this.cardSO = cardSO;
        cardArt = cardSO.cardFrontImage;
        Flip(isFlip);
    }
    public void Flip(bool isFlip)
    {

        GetComponent<Image>().sprite = isFlip ? cardArt : backArt;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (running.Choose(cardSO))
        {
            isChoosing = !isChoosing;
            if (isChoosing)
            {
                this.GetComponent<RectTransform>().position += Vector3.up * moveOffset;
                UIManager.Instance.choosingCards.Add(this);
            }
            else
            {
                this.GetComponent<RectTransform>().position -= Vector3.up * moveOffset;
                UIManager.Instance.choosingCards.Remove(this);

            }

        }
    }
    
}
