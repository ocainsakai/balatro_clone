using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class UITextAbstract : MonoBehaviour
{
    protected virtual void JumpText(TextMeshProUGUI text, string str, float duration)
    {
        text.text = str;
        text.transform.localScale = Vector3.one;
        text.transform.DOScale(1.2f, duration * 0.5f)
            .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            text.transform.DOScale(1f, duration * 0.5f)
                    .SetEase(Ease.InQuad);
        })
            .SetTarget(this);
    }
    protected virtual IEnumerator PlusNumber(TextMeshProUGUI text, int num, float duration)
    {
        int initNum = int.Parse(text.text.ToString());
        int target = initNum + num;
        text.text = $"+{num}";
        text.transform.localScale = Vector3.one;
        text.transform.DOScale(1.2f, duration * 0.5f)
            .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            text.text = target.ToString();
            text.transform.DOScale(1f, duration * 0.5f)
                    .SetEase(Ease.InQuad);
        })
            .SetTarget(this);
        yield return new WaitForSeconds(duration);
    }
}
