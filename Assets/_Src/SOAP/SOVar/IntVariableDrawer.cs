using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(IntRSO))]
public class IntVariableDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        var objectField = new ObjectField(property.displayName)
        {
            objectType = typeof(IntRSO),
        };

        objectField.BindProperty(property);
        var valueLabel = new Label();
        valueLabel.style.paddingLeft = 20f;

        container.Add(objectField); 
        container.Add(valueLabel);

        objectField.RegisterValueChangedCallback(evt => {
            var variable = evt.newValue as IntRSO;
            if (variable != null)
            {
                valueLabel.text = $"Current value: {variable.Value}";
                variable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
            } else
            {
                valueLabel.text = string.Empty ;
            }

        });

        var currentVariable = property.objectReferenceValue as IntRSO;

        if (currentVariable != null)
        {
            valueLabel.text = $"Current value: {currentVariable.Value}";
            currentVariable.OnValueChanged += newValue => valueLabel.text = $"Current value: {newValue}";
        }
       
        return container;
 
    }
}
