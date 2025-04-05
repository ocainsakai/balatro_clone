using Poker;
using UnityEngine;
using UnityEngine.UIElements;

public class PokerDisplay : MonoBehaviour
{
    UIDocument pokerDocument;
    Label pokerLabel;
    [SerializeField] PokerHandRuntime pokerhand;
    private void Awake()
    {
        pokerDocument = GetComponent<UIDocument>();
    }
    private void OnEnable()
    {
        pokerLabel = new Label($"Poker Hand\n0 x 0")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 50, left = 20 }
        };
        pokerDocument.rootVisualElement.Add( pokerLabel );
        pokerhand.OnPokerChanged += UpdatePokerDisplay;
    }
    private void OnDisable()
    {
        pokerhand.OnPokerChanged -= UpdatePokerDisplay;
    }
    public void UpdatePokerDisplay(IPokerHand poker)
    {
        string _name = Utilities.AddSpaces(poker.Name);
        pokerLabel.text = $"{_name}\n{poker.Chip} x {poker.Mult}";
    }

}
