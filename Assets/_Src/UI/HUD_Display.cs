using Blind;
using Poker;
using System;

//using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD_Display : MonoBehaviour
{
    protected UIDocument document;

    [SerializeField] IntVariable round_score;
    protected Label round_score_label;
    
    [SerializeField] BlindRSO blind_RSO;
    //[SerializeField] IntVariable baseChip;
    protected Label blind_score_label;

    [SerializeField] PokerHandRuntime pokerhand;
    protected Label poker_label;

    [SerializeField] IntVariable money;
    protected Label money_label;


    protected void Awake()
    {
        document = GetComponent<UIDocument>();
    }
    protected virtual void OnEnable()
    {
        poker_label = new Label($"Poker Hand\n0 x 0")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 80, left = 20 }
        };
        blind_score_label = new Label($"Blind Score: 0")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 20, left = 20 }

        };
        round_score_label = new Label($"Round Score: 0")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 50, left = 20 }

        };
        money_label = new Label($"Money: 0")
        {
            style = { fontSize = 24f, color = Color.white, position = Position.Absolute, top = 130, left = 20 }

        };
        document.rootVisualElement.Add(round_score_label);
        document.rootVisualElement.Add(blind_score_label);
        document.rootVisualElement.Add(poker_label);
        document.rootVisualElement.Add(money_label);

        round_score.OnValueChanged += UpdateScore;
        blind_RSO.onBlindChange += UpdateBlindRSO;
        pokerhand.OnPokerChanged += UpdatePoker;
        money.OnValueChanged += UpdateMoney;
    }

    private void UpdateScore(int arg0)
    {
        round_score_label.text = $"Round Score: {arg0}";
    }

    private void UpdateBlindRSO(IBlind arg0)
    {
        blind_score_label.text = $"Blind Score: {blind_RSO.BlindScore}";
    }

    private void UpdatePoker(IPokerHand arg0)
    {
        poker_label.text = $"{arg0.Name}\n{arg0.Chip} x {arg0.Mult}";
    }

    private void UpdateMoney(int arg0)
    {
        money_label.text = $"Money: {arg0}";

    }

    protected virtual void OnDisable()
    {
        round_score.OnValueChanged -= UpdateScore;
        blind_RSO.onBlindChange -= UpdateBlindRSO;
        pokerhand.OnPokerChanged -= UpdatePoker;
        money.OnValueChanged -= UpdateMoney;
    }
}
