using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD_Display : MonoBehaviour
{
    UIDocument document;
    [SerializeField] IntVariable round;
    Label roundLabel;


    private void Awake()
    {
        document = GetComponent<UIDocument>();
    }
    private void Start()
    {
        roundLabel = new Label($"Round: 1")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 20, left = 20f }
        };

        document.rootVisualElement.Add( roundLabel );
    }
    private void OnEnable()
    {
        round.OnValueChanged += UpdateRoundDisplay;
    }
    private void OnDisable()
    {
        round.OnValueChanged -= UpdateRoundDisplay;

    }

    private void UpdateRoundDisplay(int arg0)
    {
        roundLabel.text = $"Round: {arg0}";
    }
}
