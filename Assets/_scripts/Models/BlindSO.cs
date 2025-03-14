using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "BlindSO", menuName = "Balatro/BlindSO")]
public class BlindSO : ScriptableObject
{
    public string blindName;
    public Sprite blindIcon;
    public float scoreMultiple;
    public string effectDescrition = "no effect";
    public int reward;
    public int minimunAnte = 1;
    public bool isLocked;
    public bool isSkipped;
    public bool isDefeated;
}
