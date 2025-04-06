using UnityEngine;
using UnityEngine.UIElements;

public class ScoreDisplay : IntVarDisplayAbstract
{

    protected override void OnEnable()
    {
        label =  new Label($"Round Score: {intVariable.Value}")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 50, left = 20 }

        };
        document.rootVisualElement.Add( label );
        intVariable.OnValueChanged += UpdateDisPlay;
    }
    protected override void OnDisable()
    {
        intVariable.OnValueChanged -= UpdateDisPlay;
    }
    protected override void UpdateDisPlay(int roundScore)
    {
        label.text = $"Round Score: {roundScore}";
    }
}
