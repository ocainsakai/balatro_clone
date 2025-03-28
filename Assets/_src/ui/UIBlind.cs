using Balatro.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Balatro.Blind {
    public class UIBlind : MonoBehaviour
    {
        private TextMeshProUGUI blindNameText
                => transform.Find("name").GetComponent<TextMeshProUGUI>();
        private TextMeshProUGUI blindScore
            => transform.Find("require").GetComponent<TextMeshProUGUI>();
        private TextMeshProUGUI blindReward
            => transform.Find("reward").GetComponent<TextMeshProUGUI>();
        private Image icon => transform.Find("icon").GetComponent<Image>();
        private Button select => transform.Find("select").GetComponent<Button>();

        private TextMeshProUGUI blindDescriptionText;
        private TextMeshProUGUI requiredScoreText;

        public int _blindRequireScore;
        public void UpdateBlindInfo(BlindDataSO blindData)
        {
            blindNameText.text = blindData.blindName;
            _blindRequireScore = (int)(blindData.scoreMultiplier * 300f);
            blindScore.text = "Score at least: " + _blindRequireScore;
            blindReward.text = "Reward: $" + blindData.reward;
            icon.sprite = blindData.blindIcon;
            select.onClick.AddListener(() => GameManager.instance.StartPlayPhase(blindData, _blindRequireScore));
            select.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
            //blindDescriptionText.text = blindData.blindDescription;
            //requiredScoreText.text = $"Required Score: {blindData.requiredScore}";
        }
    }

}

