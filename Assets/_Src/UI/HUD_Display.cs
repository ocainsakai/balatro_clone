
using Balatro.Poker;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class HUD_Display : MonoBehaviour
{
    UIDocument document;

    [SerializeField] IntVariable round;
    [SerializeField] IntVariable round_score;
    [SerializeField] IntVariable ante;
    [SerializeField] PokerVariable poker;

    // A class to hold the label and its position
    private class LabelData
    {
        public Label Label { get; set; }
        public float Top { get; set; }
        public float Left { get; set; }
        public string Format { get; set; } // e.g., "Round: {0}"
        public UnityAction<int> UpdateAction { get; set; } // For IntVariable updates
        public UnityAction<PokerHand> UpdateActionPoker { get; set; } // For PokerVariable updates
    }

    // List to store all label data
    private readonly System.Collections.Generic.List<LabelData> labelDataList = new();

    private void Awake()
    {
        document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        // Define the labels and their positions
        SetupLabels();

        // Add all labels to the UI and subscribe to events
        foreach (var data in labelDataList)
        {
            document.rootVisualElement.Add(data.Label);
            SubscribeToVariable(data);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from all events
        foreach (var data in labelDataList)
        {
            UnsubscribeFromVariable(data);
        }
    }

    // Method to set up all labels
    private void SetupLabels()
    {
        labelDataList.Clear();

        // Common style settings
        float fontSize = 24f;
        Color color = Color.white;

        // Add each label with its position and format
        AddLabel(round, "Round: {0}", 20f, 20f, fontSize, color, value => $"Round: {value}");
        AddLabel(ante, "Ante: {0}", 50f, 20f, fontSize, color, value => $"Ante: {value}");
        AddLabel(round_score, "Round Score: {0}", 80f, 20f, fontSize, color, value => $"Round Score: {value}");

        AddLabel(poker, "{0}/n{0} x {0}", 110f, 20f, fontSize, color, value => $"{value}");
       
    }

    // Generic method to create and configure a label for an IntVariable
    private void AddLabel(IntVariable variable, string format, float top, float left, float fontSize, Color color, System.Func<int, string> formatFunc)
    {
        
        var label = new Label(format.Replace("{0}", "0"))
        {
            style = { fontSize = fontSize, color = color, position = Position.Absolute, top = top, left = left }
        };

        var data = new LabelData
        {
            Label = label,
            Top = top,
            Left = left,
            Format = format,
            UpdateAction = value => label.text = formatFunc(value)
        };

        labelDataList.Add(data);
    }

    // Overloaded method for PokerVariable (which likely uses strings)
    private void AddLabel(PokerVariable variable, string format, float top, float left, float fontSize, Color color, System.Func<string, string> formatFunc)
    {
        var combinedLabel = new Label($"{variable.poker.Name}\n{variable.poker.Chip} x {variable.poker.Mult}")
        {
            style = {
            fontSize = fontSize,
            color = color,
            position = Position.Absolute,
            top = top, 
            left = left,  
            unityTextAlign = TextAnchor.UpperLeft
            }
        };

        var data = new LabelData
        {
            Label = combinedLabel,
            Top = top,
            Left = left,
            Format = format,
            UpdateActionPoker = value => combinedLabel.text = formatFunc(value.Name)
        };

        labelDataList.Add(data);
    }

    // Subscribe to the variable's OnValueChanged event
    private void SubscribeToVariable(LabelData data)
    {
        if (data.UpdateAction != null)
        {
            // Find the corresponding IntVariable
            var intVariable = GetIntVariableForLabel(data);
            if (intVariable != null)
            {
                intVariable.OnValueChanged += data.UpdateAction;
                data.UpdateAction(intVariable.Value); // Set initial value
            }
        }
        else if (data.UpdateActionPoker != null)
        {
            // Find the corresponding PokerVariable
            var pokerVariable = GetPokerVariableForLabel(data);
            if (pokerVariable != null)
            {
                pokerVariable.OnValueChanged += data.UpdateActionPoker;
                data.UpdateActionPoker(pokerVariable.poker); // Set initial value
            }
        }
    }

    // Unsubscribe from the variable's OnValueChanged event
    private void UnsubscribeFromVariable(LabelData data)
    {
        if (data.UpdateAction != null)
        {
            var intVariable = GetIntVariableForLabel(data);
            if (intVariable != null)
            {
                intVariable.OnValueChanged -= data.UpdateAction;
            }
        }
        else if (data.UpdateActionPoker != null)
        {
            var pokerVariable = GetPokerVariableForLabel(data);
            if (pokerVariable != null)
            {
                pokerVariable.OnValueChanged -= data.UpdateActionPoker;
            }
        }
    }

    // Helper to find the corresponding IntVariable for a label
    private IntVariable GetIntVariableForLabel(LabelData data)
    {
        if (data.Format.Contains("Round:") && !data.Format.Contains("Score")) return round;
        if (data.Format.Contains("Round Score:")) return round_score;
        if (data.Format.Contains("Ante:")) return ante;
        return null;
    }

    // Helper to find the corresponding PokerVariable for a label
    private PokerVariable GetPokerVariableForLabel(LabelData data)
    {
        if (data.Format.Contains("Poker:")) return poker;
        return null;
    }
}
