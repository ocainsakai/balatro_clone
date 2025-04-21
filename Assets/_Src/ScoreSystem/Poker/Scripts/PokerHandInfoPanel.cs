using UnityEngine;
using UnityEngine.UIElements;

public class PokerHandInfoPanel : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private Label titleLabel;
    private Label scoreLabel;
    private Label multiplierLabel;
    private void Awake()
    {
        var root = uiDocument.rootVisualElement;
        titleLabel = root.Q<Label>(className: "hand-title");
        scoreLabel = root.Q<Label>(className: "score-box");
        multiplierLabel = root.Q<Label>(className: "multiplier-box");

    }
    public void Show(PokerHandData data)
    {
        if (data == null) return;

        titleLabel.text = data.displayName;
        scoreLabel.text = data.baseScore.ToString();
        multiplierLabel.text = data.baseMultiplier.ToString();
    }
}