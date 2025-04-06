using Blind;
using UnityEngine;
using UnityEngine.UIElements;

public class BlindScoreDisplay : IntVarDisplayAbstract
{
    [SerializeField] BlindRSO blindRSO;
    protected override void OnEnable()
    {

        label = new Label($"Blind Score: 0")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 20, left = 20 }

        };
        document.rootVisualElement.Add(label);
        blindRSO.onBlindChange += UpdateDisPlay;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        intVariable.OnValueChanged -= UpdateDisPlay;
    }
    protected void UpdateDisPlay(IBlind blind)
    {
        label.text = $"Blind Score: {blindRSO.BlindScore}";
    }
}
