using Balatro.Cards.System;
using UnityEngine;

public class DeckView : MonoBehaviour
{
    [SerializeField] CardManager cardManager;
    [SerializeField] GameObject deckUi;

    bool isShowed;

    private void Start()
    {
        isShowed = true;
        Toggle();
    }
    public void Toggle()
    {
        isShowed = !isShowed;
        deckUi.gameObject.SetActive(isShowed);
    }
    private void OnMouseDown()
    {
        Toggle();
    }
}
