
using DG.Tweening;
using UnityEngine;

public abstract class BaseAnim : MonoBehaviour
{
    [Header("Animation Settings")]
    //[SerializeField] protected bool useAnimation = true;
    [SerializeField] protected float animationDuration = 0.2f;
    [SerializeField] protected Ease animationEase = Ease.InOutQuad;


    //private static Queue<Func<UniTask>> tasks = new Queue<Func<UniTask>>();
    //private static bool isProcessing = false;
    //public static void AddAnim(Func<UniTask> anim)
    //{
    //    tasks.Enqueue(anim);
    //    if (!isProcessing)
    //    {
    //        _ = ExecuteTasks();
    //    }
    //}
    //private static async UniTask ExecuteTasks()
    //{
    //    isProcessing = true;
    //    while (tasks.Count > 0)
    //    {
    //        Func<UniTask> task = tasks.Dequeue();
    //        await task();
    //    }
    //    isProcessing = false;
    //}
}