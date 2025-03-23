using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "BlindSO", menuName = "Balatro/BlindSO")]
public class Blind : ScriptableObject
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
#if UNITY_EDITOR
    private void GetData()
    {
        string first = name.Split(' ')[0];
        string second = name.Split(' ')[1];
        blindIcon = Resources.Load<Sprite>($"PNG/{first}_{second}");

    }
    protected void OnValidate()
    {
        GetData();
    }
#endif
}
