using PhaseSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BetPhaseUI : BasePhaseUI
{
    [SerializeField] GameObject betPanel;
    public event UnityAction<BetOption> OnBetPhaseClick;
    public void UpdateBetPanel(List<BetOption> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            var option = options[i];
            var optionUI = betPanel.transform.GetChild(i);
            var optionBtn = optionUI.GetComponent<Button>();
            optionBtn.onClick.AddListener(() => OnBetPhaseClick?.Invoke(option));

            var optionText = optionUI.GetComponentInChildren<TextMeshProUGUI>();
            optionText.text = $"{option.betName}\n" +
                            $"Blind: {option.requiredScore}\n" +
                            $"Reward: $3";
        }
    }
    public override void Hide()
    {
       betPanel.SetActive(false);
    }

    public override void Show()
    {
        Debug.Log("bet ui show");
        betPanel.SetActive(true);
    }
}
