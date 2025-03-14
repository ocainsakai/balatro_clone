using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnteSO", menuName = "Scriptable Objects/AnteSO")]
public class AnteSO : ScriptableObject
{
    public List<Ante> antes;
}
[System.Serializable]
public class Ante
{
    public int level;
    public int baseChipsRequirement;
    public int baseChips_GreenOrHigher;
    public int baseChips_PurpleOrHigher;
}
