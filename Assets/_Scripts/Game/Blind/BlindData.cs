using UnityEngine;
[CreateAssetMenu(fileName = "BlindData", menuName = "Scriptable Objects/BlindData")]

public class BlindData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Artwork;
    public int MinAnte;
    public float ScoreMultiple;
    public int Reward;
}
