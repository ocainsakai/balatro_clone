using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Transform textEffect;
    public async UniTask AddMultEffect(Transform pos, int value)
    {
        var tmp = textEffect.GetComponentInChildren<TextMeshPro>();
        if (tmp == null)
        {
            return;
        }

        tmp.text = $"+{value}";
        tmp.color = Color.red;
        await MoveUpEffect(pos);
    }
    public async UniTask AddChipEffect(Transform pos, int value)
    {
        var tmp = textEffect.GetComponentInChildren<TextMeshPro>();
        if (tmp == null)
        {
            return;
        }
        tmp.text = $"+{value}";
        tmp.color = Color.blue;
        await MoveUpEffect(pos);
    }

    private async UniTask MoveUpEffect(Transform pos)
    {
        textEffect.transform.position = pos.position + Vector3.up;
        textEffect.gameObject.SetActive(true);

        float yTarget = textEffect.transform.position.y + 0.5f;
        await textEffect.transform
                        .DOMoveY(yTarget, 0.1f)
                        .SetEase(Ease.OutQuad)
                        .AsyncWaitForCompletion()
                        .AsUniTask();

        await UniTask.Delay(1000);
        textEffect.gameObject.SetActive(false);
    }


}
