using UnityEngine;

[CreateAssetMenu(fileName = "JokerCardData", menuName = "Scriptable Objects/JokerCardData")]
public class JokerCardData : ScriptableObject
{
    [Header("Basic Info")]
    public string JokerName;
    [TextArea]
    public string Description;
    public Sprite Art;

    [Header("Gameplay")]
    public int Price;
    public int SellValue;
    [Header("Tags")]
    public bool IsLegendary;
    public bool IsPassive;
}
