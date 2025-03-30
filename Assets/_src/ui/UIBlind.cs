using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Balatro.Blind {
    public class UIBlind : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameBlind;
        [SerializeField] Image iconBlind;
        [SerializeField] TextMeshProUGUI scoreBlind;
        [SerializeField] TextMeshProUGUI rewardBlind;
        [SerializeField] Button selectBtn;
        [SerializeField] TextMeshProUGUI selectBtnTxt;
        public bool isDefeated;
        public bool isSkipped;
        public bool isLocked;

        public void Initlize(BlindDataSO blindDataSO, int baseChips)
        {
            isLocked = true;
            isDefeated = false;
            isSkipped = false;
            nameBlind.text = blindDataSO.name;
            iconBlind.sprite = blindDataSO.blindIcon;
            int blindScore = (int) (baseChips * blindDataSO.scoreMultiplier);
            scoreBlind.text = $"Score at least: \n{blindScore}";
            rewardBlind.text = $"Reward: ${blindDataSO.reward}";
            selectBtnTxt.text = "Locked";
            selectBtn.onClick.RemoveAllListeners();
        }
        public void Defeat()
        {
            isDefeated=true;
            selectBtn.onClick.RemoveAllListeners();
            selectBtnTxt.text = "Defeated";
        }
        public void Skip()
        {
            isSkipped=true;
            selectBtn.onClick.RemoveAllListeners();
            selectBtnTxt.text = "Skipped";
        }
        public void SelectBlind(UnityAction action)
        {
            isLocked = false;
            selectBtn.onClick.AddListener(action);
            selectBtnTxt.text = "Select";
        }
    }

}

