using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class AsyncProcess
{

    private readonly Queue<Func<Task>> queue = new();
    public ReactiveProperty<bool> isProcessing = new (false);
    public Action OnCompelte;
    public void Enqueue(Func<Task> taskFunc, Action onComplete = null)
    {
        //Debug.Log("enqueue");
        if (onComplete != null) { OnCompelte = onComplete; }    
        queue.Enqueue(taskFunc);
        if (!isProcessing.Value)
        {
            _ = ExecuteQueueAsync(onComplete);
        }
    }

    private async Task ExecuteQueueAsync(Action onComplete)
    {
        isProcessing.Value = true;

        while (queue.Count > 0)
        {
            var taskFunc = queue.Dequeue();
            await taskFunc();
        }
        
        isProcessing.Value = false;
        OnCompelte?.Invoke();
    }

    public void Clear()
    {
        queue.Clear();
        isProcessing.Value = false;
    }
}