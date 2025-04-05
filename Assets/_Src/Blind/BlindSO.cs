using UnityEngine;

[CreateAssetMenu(fileName = "BlindSO", menuName = "SO/BlindSO")]
public class BlindSO : ScriptableObject, IBlind
{
    public IBlind data => this;

    [SerializeField] string _name;
    public string Name => _name;
    [SerializeField] Sprite _sprite;
    public Sprite Sprite => _sprite;
    [SerializeField] int _minimunAnte;
    public int minimunAnte => _minimunAnte;
    [SerializeField] float _scoreMultiple;
    public float scoreMultiple => _scoreMultiple;
    [SerializeField] int _reward;
    public int reward => _reward;

    public int BaseChips => 0;

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(this.name))
        {
            string[] parts = this.name.Split(' ');

            if (parts.Length == 2)
            {
                _name = this.name;
                _sprite = Resources.Load<Sprite>($"PNG/{parts[0]}_{parts[1]}");
            }
        }
    }
}
