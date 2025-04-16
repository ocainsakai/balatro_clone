using Balatro.Combo;
using System.Collections.Generic;
using UnityEngine;

namespace Balatro.Combo
{
    [CreateAssetMenu(fileName = "ComboDatabase", menuName = "Scriptable Objects/ComboDatabase")]
    public class ComboDatabase : ScriptableObject
    {
        public List<ComboTypeData> comboTypes;
    }

}
