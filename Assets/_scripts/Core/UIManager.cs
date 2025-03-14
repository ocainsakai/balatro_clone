using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [Header("Card")]
    [SerializeField] private Transform handArea;
    [SerializeField] private Transform jokerArea;
    [SerializeField] private Transform consumableArea;
    [SerializeField] private GameObject cardPrf;

    [Header("Blind")]
    [SerializeField] private BlindManager blindContainerUI;
    [SerializeField] private BlindUI small;
    [SerializeField] private BlindUI big;
    [SerializeField] private BlindUI boss;

    [Header("Run")]
    [SerializeField] public RunUI runUI;
    [SerializeField] private GameObject playActionUI;
    [SerializeField] private GameObject shopUI;


    public List<CardUI> choosingCards;
    private DeckManager deck => DeckManager.Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    private void Reset()
    {
#if UNITY_EDITOR 
        //List<BlindUI> blinds = new List<BlindUI> (GetComponentsInChildren<BlindUI>());
        //Debug.Log(blinds.Count);
        //small = blinds.FirstOrDefault(t => t.name.Contains("Small"));
        //big = blinds.FirstOrDefault(t => t.name.Contains("Big"));
        //boss = blinds.FirstOrDefault(t => t.name.Contains("Boss"));
#endif
    }

    public void UpdatePhaseUI(Phase currentPhase)
    {
        HideAll();
        switch (currentPhase)
        {
            case Phase.Blind:
                blindContainerUI.Show();
                break;
            case Phase.Score:
                playActionUI.SetActive(true);
                handArea.gameObject.SetActive(true);
                jokerArea.gameObject.SetActive(true);
                consumableArea.gameObject.SetActive(true);
                deck.Show();
                break;
            case Phase.Shop:
                shopUI.SetActive(true);
                break;
        }
    }

    private void HideAll()
    {
        blindContainerUI.Hide();
        playActionUI.SetActive(false);
        shopUI.SetActive(false);

        handArea.gameObject.SetActive(false);
        jokerArea.gameObject.SetActive(false);
        consumableArea.gameObject.SetActive(false);
    }
    public void UpdateBlindUI(BlindSO[] blinds)
    {
        int baseChips = GameManager.Instance.running.GetBaseChips();
        small.SetBlind(blinds[0], baseChips);
        big.SetBlind(blinds[1], baseChips);
        boss.SetBlind(blinds[2], baseChips);
    }
    public void DrawHand(List<CardSO> cards)
    {
        cards.ForEach(DrawHand);
    }
    public void DrawHand(CardSO card)
    {
        GameObject newCard = Instantiate(cardPrf);
        newCard.transform.SetParent(handArea);
        newCard.transform.localScale = Vector3.one;
        newCard.GetComponent<CardUI>().SetCard(card);
    }
    public void DestroyCard()
    {
        foreach (CardUI item in choosingCards)
        {
            Destroy(item.gameObject);
        }
        choosingCards.Clear();
    }
}
