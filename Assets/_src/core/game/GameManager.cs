using Balatro.Core.Card;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] DeckManager deckManager;
    [SerializeField] HandManager handManager;

    public void Awake()
    {
    }
    public void Start()
    {
        deckManager.InitializeDeck();
        DrawHand();
    }
    public void DrawHand()
    {
        for (int i = handManager.hand_count; i < handManager.MAX_HAND_SIZE; i++)
        {
            CardDataSO card = deckManager.DrawCard();
            handManager.AddCardToHand(card);
        }
        handManager.Sort();
    }
    public void Discard()
    {
        StartCoroutine(DiscardProcess());
    }
    private IEnumerator DiscardProcess()
    {
        yield return handManager.RemoveSelected();
        DrawHand();

    }
    public void PlayHand()
    {
        StartCoroutine(PlayHandProcess());
    }
    private IEnumerator PlayHandProcess()
    {
        yield return StartCoroutine(handManager.CalculateSelected());
        DrawHand();
    }
}
