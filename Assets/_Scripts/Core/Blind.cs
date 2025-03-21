using UnityEngine;


[CreateAssetMenu(fileName = "Blind", menuName = "Scriptable Objects/Blind")]
public class Blind : ScriptableObject
{
    public int minAnte;
    public float score_multiple;
    public int reward;
    public Sprite artwork;
#if UNITY_EDITOR
    private void GetData()
    {
        string first = name.Split(' ')[0];
        string second = name.Split(' ')[1];
        artwork = Resources.Load<Sprite>($"PNG/{first}_{second}");

    }
    protected void OnValidate()
    {
        GetData();
    }
#endif
}