using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardSO cardSO;
    public DeckManager deckManager;
    public float moveOffset = 10f;
    private bool choosing;
    
    public void SetImage()
    {

        GetComponentInChildren<Image>().sprite = cardSO.artWork;

    }
    public void Choose()
    {

        if (deckManager.canChoose && !choosing)
        {
            deckManager.Choose(this);
            this.GetComponent<RectTransform>().position += Vector3.up * moveOffset;
            choosing = !choosing;
        } else
        if (choosing)
        {
            deckManager.GiveUp(this);
            this.GetComponent<RectTransform>().position -= Vector3.up * moveOffset;
            choosing = !choosing;
        }
    }
    
}
