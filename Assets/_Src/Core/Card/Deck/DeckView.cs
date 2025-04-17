
using Balatro.Cards.System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckView : MonoBehaviour
{
    [SerializeField] PlayManager cardManager;
    [SerializeField] GameObject deckUi;
    [SerializeField] GameObject cardUIPrf;
    bool isShowed;
    

    private void Awake()
    {
        isShowed = true;
        Toggle();
    }
    private void Deck_OnDeckChanged()
    {
        //var cards = cardManager.deck.GetCards();
        //Debug.Log(cards[0].Art.name);
        //if (cards ==  null) return;
        //var arts = cards.Select(x => x.Art).ToList();
        ////Debug.Log(arts[0].name);
        //ModifiedCardsUI(arts, deckUi.transform);
        //ReverseChildDisplay();
    }
    public void Toggle()
    {
        isShowed = !isShowed;
        deckUi.gameObject.SetActive(isShowed);
        if (isShowed)
        {
            Deck_OnDeckChanged();
        }
    }
    void ReverseChildDisplay()
    {
        int count = deckUi.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            deckUi.transform.GetChild(i).SetSiblingIndex(i);
        }
    }
    void ModifiedCardsUI(List<Sprite> arts, Transform parent)
    {
        int amount = arts.Count;
        if (cardUIPrf == null || parent == null)
        {
            Debug.LogWarning("Missing references.");
            return;
        }

        // Remove extra
        for (int i = parent.childCount - 1; i >= amount; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

        // Add missing
        while (parent.childCount < amount)
        {
            Instantiate(cardUIPrf, parent);
        }
        for (int i = 0; i < arts.Count; i++)
        {
            parent.GetChild(i).GetComponent<Image>().sprite = arts[i];
        }

    }
    private void OnMouseDown()
    {
        Toggle();
    }
}
