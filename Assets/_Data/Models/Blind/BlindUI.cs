using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlindUI : MonoBehaviour
{
    [SerializeField] private Image art;
    [SerializeField] private TextMeshProUGUI reward;
    public void UpdateBlind(BlindSO blind)
    {
        //transform.Find("name").GetComponent<TextMeshProUGUI>().text = ;
        art.sprite = blind.art;
        reward.text = "reward: " + blind.reward;

    }
}
