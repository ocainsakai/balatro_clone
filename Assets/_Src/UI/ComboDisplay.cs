
using Balatro.Combo;
using UnityEngine;
using UnityEngine.UIElements;

public class ComboDisplay : MonoBehaviour
{
    UIDocument document;
    Label comboNameLabel;
    Label chipLabel;
    Label multLabel;
    [SerializeField] ComboVariable comboVariable;
    private void Awake()
    {
        document = GetComponent<UIDocument>();

    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        comboNameLabel = new Label("Poker Name")
        {
            style = { fontSize = 8, color = Color.white,
            unityTextAlign = TextAnchor.MiddleCenter,
            whiteSpace = WhiteSpace.NoWrap,
            marginBottom = 0,
            }
        };
        chipLabel = new Label("0")
        {
            style = { fontSize = 8, color = Color.blue }

        };
        multLabel = new Label("0")
        {
            style = { fontSize = 8, color = Color.red }
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
        chipRow.Add(new Label("x") { style = { fontSize = 8, color = Color.black } });
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
        
        document.rootVisualElement.Add(container);

        comboVariable.OnComboTypeChange += UpdateComboName;
        comboVariable.chip.OnValueChanged += UpdateChip;
        comboVariable.mult.OnValueChanged += UpdateMult;
    }
    private void OnDisable()
    {
        comboVariable.OnComboTypeChange -= UpdateComboName;
        comboVariable.chip.OnValueChanged -= UpdateChip;
        comboVariable.mult.OnValueChanged -= UpdateMult;
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
