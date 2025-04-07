
using Core;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject actionBtn, blindPanel, shopPanel;

    public void UpdateUI(GameState state)
    {
        blindPanel.SetActive(state == GameState.Blinding);
        actionBtn.SetActive(state != GameState.Blinding && state != GameState.Shopping);
        shopPanel.SetActive(state == GameState.Shopping);
    }
}
