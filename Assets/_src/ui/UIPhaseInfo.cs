using Balatro.Blind;
using Balatro.Core;
using TMPro;
using UnityEngine;

public class UIPhaseInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    public void UpdatePhase(Phase phase)
    {
        switch (phase)
        {
            case Phase.SelectPhase:
                text.text = "Choose your next blind";
                break;
            case Phase.PlayPhase:
                text.text = $"";
                break;
            case Phase.ShopPhase:
                text.text = $"";
                break;  
        }
    }
    public void ShowBlind(BlindDataSO blindData, int blindScore)
    {
        text.text = $"{blindData.blindName}\n{blindScore}";
    }

}
