using UnityEngine;
using UnityEngine.UIElements;

public class ScoreDisplay : MonoBehaviour
{
    UIDocument scoreDocument;
    Label scoreLabel;
    [SerializeField] IntVariable roundScore;

    private void Awake()
    {
         scoreDocument = GetComponent<UIDocument>();
    }
    private void OnEnable()
    {
        scoreLabel =  new Label($"Round Score: {roundScore.Value}")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 20, left = 20 }

        };
        scoreDocument.rootVisualElement.Add( scoreLabel );
        roundScore.OnValueChanged += UpdateScoreDisPlay;
    }
    private void OnDisable()
    {
        roundScore.OnValueChanged -= UpdateScoreDisPlay;
    }
    void UpdateScoreDisPlay(int roundScore)
    {
        scoreLabel.text = $"Round Score: {roundScore}";
    }
}
