using UnityEngine;

public class Card : MonoBehaviour
{
    public CardSO cardSO;
    public DeckManager deckManager;

    private bool choosing;
    
    public void SetImage()
    {

        GetComponentInChildren<SpriteRenderer>().sprite = cardSO.artWork;

    }
    public void Choose()
    {
        if (deckManager.canChoose && !choosing)
        {
            deckManager.Choose(this);
            this.transform.position += Vector3.up;
            choosing = !choosing;
        } else
        if (choosing)
        {
            deckManager.GiveUp(this);
            this.transform.position -= Vector3.up;
            choosing = !choosing;
        }
    }
    
}
