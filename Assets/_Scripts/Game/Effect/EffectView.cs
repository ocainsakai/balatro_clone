using DG.Tweening;
using Game.System.Score;
using TMPro;
using UnityEngine;
using VContainer;
using UniRx;
using Game.Cards;

public class EffectView : MonoBehaviour {
    [SerializeField] TextMeshPro effectPrefab;
    [Inject]
    public void Construct(ScoreManager scoreManager, CardFactory cardFactory)
    {
        scoreManager.OnScore.Subscribe(card =>
        {
            var cardView = cardFactory.GetCardFormID(card.CardID);
            string text = $"+{Card._cards[card.CardID].Data.Value}";
            ShowText(text, cardView.transform.position + Vector3.up);
        });
    }
    public void ShowText(string text, Vector2 position)
    {
        effectPrefab.transform.position = position;
        effectPrefab.text = text;
        effectPrefab.gameObject.SetActive(true);
        effectPrefab.transform.DOMoveY(transform.position.y + 1f, 0.2f)
            .OnComplete( () => { effectPrefab.gameObject.SetActive(false); });
    }
}
