using UnityEngine;

public class UIManager : AbstractSingleton<UIManager>
{
    [SerializeField] private GameObject handArea;
    [SerializeField] private GameObject actionBtns;
    [SerializeField] private GameObject blindArea;
    [SerializeField] private GameObject deck;
    public void ShowHand()
    {
        //HideAll();
        handArea.SetActive(true);
    }

    public void ShowAction()
    {
        //HideAll();
        actionBtns.SetActive(true);
    }

    public void ShowBlind()
    {
        //HideAll();
        blindArea.SetActive(true);
    }
    public void ShowDeck()
    {
        deck.SetActive(true);
    }
    public void HideAll()
    {
        deck.SetActive(false);
        handArea.SetActive(false);
        actionBtns.SetActive(false);
        blindArea.SetActive(false);
    }
}
