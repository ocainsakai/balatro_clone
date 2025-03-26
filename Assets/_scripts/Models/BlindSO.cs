using UnityEngine;

[CreateAssetMenu(fileName = "BlindSO", menuName = "Balatro/BlindSO")]
public class BlindSO : ScriptableObject
{

    public Sprite blindIcon;
    public float scoreMultiple;
    public string effectDescrition = "no effect";
    public int reward;
    public int minimunAnte = 1;
    //public bool isLocked;
    //public bool isSkipped;
    //public bool isDefeated;
#if UNITY_EDITOR
    private void GetData()
    {
        var token = this.name?.Split(' ');
        blindIcon = token?.Length >= 2 ? Resources.Load<Sprite>($"PNG/{token[0]}_{token[1]}") : null;

    }
    protected void OnValidate()
    {
        GetData();
    }
#endif
}
