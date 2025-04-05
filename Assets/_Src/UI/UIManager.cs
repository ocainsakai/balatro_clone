using Card;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public IntVariable playerChips;
    public IntVariable blindHealth;
    public TextMeshProUGUI chipsText;
    public TextMeshProUGUI blindHealthText;
    public Transform handPanel;
    public GameObject cardPrefab;

    //public void UpdateChips() { chipsText.text = $"Chips: {playerChips.value}"; }
    //public void UpdateBlindHealth() { blindHealthText.text = $"Blind HP: {blindHealth.value}"; }

    public void DisplayCard(ICard card) 
    {
        GameObject cardObj = Instantiate(cardPrefab, handPanel);
        cardObj.GetComponentInChildren<TextMeshProUGUI>().text = card.Name;
        // Thêm logic chơi bài khi click
    }
}
