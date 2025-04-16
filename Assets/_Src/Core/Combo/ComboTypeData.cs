using UnityEngine;

namespace Balatro.Combo
{
    [CreateAssetMenu(fileName = "ComboTypeData", menuName = "Scriptable Objects/ComboTypeData")]

    public class ComboTypeData : ScriptableObject
    {
        public string Name { get; private set; }
        public int Chip;
        public int Mult;

        private void OnValidate()
        {
            //var _name = 
            this.Name = AddSpacesToSentence(name);
        }
        private string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var newText = System.Text.RegularExpressions.Regex.Replace(text, "(?<!^)([A-Z])", " $1");
            return newText.Trim();
        }

    }
}

