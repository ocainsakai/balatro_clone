using TMPro;
using UnityEngine;

namespace Balatro.UI
{
    public class UIRoundScore : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _roundScore;
        public int _totalScore = 0;
        public void SetScore(int score)
        {
            _totalScore = score;
            _roundScore.text = "Round Score: " + _totalScore.ToString();
        }
        public void PlusScore(int score)
        {
            _totalScore += score;
            _roundScore.text = "Round Score: " + _totalScore.ToString();

        }
    }

}
