using DG.Tweening;
using TMPro;
using UnityEngine;

public class EffectView : MonoBehaviour {
    [SerializeField] TextMeshPro effectPrefab;

    public void ShowText(string text, Vector2 position)
    {
        effectPrefab.transform.position = position;
        effectPrefab.text = text;
        effectPrefab.gameObject.SetActive(true);
        effectPrefab.transform.DOMoveY(transform.position.y + 1f, 0.2f)
            .OnComplete( () => { effectPrefab.gameObject.SetActive(false); });
    }
}
