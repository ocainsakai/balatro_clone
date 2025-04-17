
using Balatro.Combo;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD_Display : MonoBehaviour
{
    UIDocument document;
    Label comboNameLabel;
    Label chipLabel;
    Label multLabel;
    Label scoreLabel;
    Label blindLabel;
    [SerializeField] ComboTypeRSO comboVariable;
    [SerializeField] IntRSO total_score;
    [SerializeField] IntRSO blind_score;

    public int font_size = 24;
    private void Awake()
    {
        document = GetComponent<UIDocument>();

    }
    private void OnEnable()
    {

        scoreLabel = new Label($"Score: {total_score.Value}")
        {
            style = { fontSize = font_size, color = Color.white }

        };
        blindLabel = new Label($"Blind: {blind_score.Value}")
        {
            style = { fontSize = font_size, color = Color.white }

        };
        comboNameLabel = new Label("Poker Name")
        {
            style = { fontSize = font_size, color = Color.white,
            unityTextAlign = TextAnchor.MiddleCenter,
            whiteSpace = WhiteSpace.NoWrap,
            marginBottom = 0,
            }
        };
        chipLabel = new Label("0")
        {
            style = { fontSize = font_size, color = Color.blue }

        };
        multLabel = new Label("0")
        {
            style = { fontSize = font_size, color = Color.red }
        };

        var chipRow = new VisualElement()
        {
            style = { flexDirection = FlexDirection.Row, 
            alignContent = Align.Center,
            justifyContent = Justify.Center,
            alignSelf = Align.Center,
            marginTop = 0,
            }
        };
        chipRow.Add(chipLabel);
        chipRow.Add(new Label("x") { style = { fontSize = font_size, color = Color.black } });
        chipRow.Add(multLabel);

        var container = new VisualElement()
        {
            style = { 
            flexDirection = FlexDirection.Column,
            alignItems = Align.Center,
            justifyContent = Justify.FlexStart,
            position = Position.Absolute,
            top = 10f, left = 10f
            }
        };
        container.Add(comboNameLabel);
        container.Add(chipRow);
        container.Add(blindLabel);
        container.Add(scoreLabel);

        document.rootVisualElement.Add(container);

        comboVariable.OnComboTypeChange += UpdateComboName;
        comboVariable.chip.OnValueChanged += UpdateChip;
        comboVariable.mult.OnValueChanged += UpdateMult;

        total_score.OnValueChanged += UpdateScore;
        blind_score.OnValueChanged += UpdateBlind;
    }

    private void UpdateBlind(int arg0)
    {
        Debug.Log("blind Chnaged");
        blindLabel.text = $"Blind: {arg0}";

    }

    private void UpdateScore(int arg0)
    {
        scoreLabel.text = $"SCORE: {arg0}";
    }

    private void OnDisable()
    {
        comboVariable.OnComboTypeChange -= UpdateComboName;
        comboVariable.chip.OnValueChanged -= UpdateChip;
        comboVariable.mult.OnValueChanged -= UpdateMult;

        total_score.OnValueChanged -= UpdateScore;
        blind_score.OnValueChanged -= UpdateBlind;
    }
    void UpdateChip(int chip)
    {
        chipLabel.text = chip.ToString();
    }
    void UpdateMult(int mult)
    {
        multLabel.text = mult.ToString();
    }
    private void UpdateComboName(ComboTypeData comboType)
    {
        comboNameLabel.text = comboType.Name;
    }
}
