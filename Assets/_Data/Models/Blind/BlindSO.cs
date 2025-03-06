using UnityEngine;

[CreateAssetMenu(fileName = "BlindSO", menuName = "Scriptable Objects/BlindSO")]
public class BlindSO : ScriptableObject
{
    public string description;
    public int minimun_ante;
    public float score_multiple;
    public int reward;
    public Sprite art;
}
