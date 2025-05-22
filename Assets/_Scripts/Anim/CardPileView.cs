using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CardPileView : BaseAnim
{
    public async UniTask SequentialReposition(int delayMs)
    {
        foreach (Transform child in transform)
        {
            if (child.transform.position == transform.position) continue;
            if (!DOTween.IsTweening(child))
            {
                child.DOMove(transform.position, animationDuration)
                                .SetEase(animationEase);
                child.GetComponent<Card>().Flip(false);
            }

            await UniTask.Delay(delayMs);
        }
    }

    public async UniTask SequentialReposition()
    {
        foreach (Transform child in transform)
        {
            if (!DOTween.IsTweening(child))
            {
                await child.DOMove(transform.position, animationDuration)
                           .SetEase(animationEase)
                           .AsyncWaitForCompletion();
            }
        }
    }
    public UniTask ParallelReposition()
    {
        List<UniTask> tasks = new List<UniTask>();

        foreach (Transform child in transform)
        {
            if (!DOTween.IsTweening(child))
            {
                tasks.Add(
                    child.DOMove(transform.position, animationDuration)
                         .SetEase(animationEase)
                         .AsyncWaitForCompletion()
                         .AsUniTask()
                );
            }
        }

        return UniTask.WhenAll(tasks);
    }
}
