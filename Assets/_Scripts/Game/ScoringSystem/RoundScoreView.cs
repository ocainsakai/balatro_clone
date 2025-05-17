using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using UniRx;
namespace Game.System.Score
{
    public class RoundScoreView : MonoBehaviour
    {
        [Inject] ScoreSystem scoreManager;
        private UIDocument document;
        private Label roundScore;

        private void Awake()
        {
            document = GetComponent<UIDocument>();
            roundScore = document.rootVisualElement.Query<Label>("ScoreText");

            scoreManager.RoundScore.Subscribe(x => { roundScore.text = x.ToString(); });
        }
    }
}