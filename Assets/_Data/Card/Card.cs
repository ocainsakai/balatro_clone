using UnityEngine;

public class Card : MonoBehaviour
{
    public CardSO cardSO;
    public DeckManager deckManager;
    private Transform outline;

    private bool choosing;
    
    private void Awake()
    {
        outline = transform.Find("Outline");
        outline.GetComponent<SpriteRenderer>().color = Color.blue;
        outline.gameObject.SetActive(false);
    }
    public void SetImage()
    {
        if(cardSO != null)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = cardSO.artWork;
        }
        
    }
    public void Choose()
    {
        if (deckManager.canChoose && !choosing)
        {
            deckManager.Choose(this);
            outline.gameObject.SetActive(true);
            choosing = !choosing;
        } else
        if (choosing)
        {
            deckManager.GiveUp(this);
            outline.gameObject.SetActive(false);
            choosing = !choosing;
        }
    }
    
}
