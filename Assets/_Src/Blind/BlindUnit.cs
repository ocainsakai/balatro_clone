using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Blind
{
    public class BlindUnit : MonoBehaviour
    {
        [SerializeField] IntVariable baseChip;
        [SerializeField] Button selectBtn;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI blind_name;
        [SerializeField] TextMeshProUGUI blind_score;
        public BlindSO Data;

        public Action<BlindUnit> OnSelect;
        private BlindState _state;
        public BlindState state
        {
            get => this._state;
            set
            {
                if (this._state == value) return;
                this._state = value;
                switch (value)
                {
                    case BlindState.Ready:
                        UpdateButtonText("Select");
                        selectBtn.onClick.AddListener(() => OnSelect?.Invoke(this));
                        break;
                    default:
                        UpdateButtonText(value.ToString());
                        selectBtn.onClick.RemoveAllListeners();
                        break;
                }
                UpdateBlindDisplay(Data);
                //OnStateChange?.Invoke(value);
            }
        }
        public void UpdateButtonText(string text)
        {
            var textMesh = selectBtn.GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = text;
        }
        public void UpdateBlindDisplay(IBlind blind)
        {
            icon.sprite = blind.Sprite;
            blind_name.text = blind.Name;
            var score = (int)(baseChip.Value * blind.scoreMultiple);
            this.blind_score.text = $"Score at least: {score}";
        }
    }
    public enum BlindState
    {
        None,
        Locked,
        Ready,
        Skipped,
        Defeated
    }
}
