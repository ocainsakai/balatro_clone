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
            this.Name = TxtUltil.AddSpacesToSentence(name);
        }

    }
}

