using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process
{
    public Queue<IEnumerator> queue = new Queue<IEnumerator>();
    public bool isProcessing = false;
    public void Anim(MonoBehaviour mono, IEnumerator enumerator)
    {
        queue.Enqueue(enumerator);
        if (!isProcessing)
        {
            mono.StartCoroutine(Execute());
        }

    }
    public IEnumerator Execute()
    {
        isProcessing = true;
        while (queue.Count > 0)
        {
            yield return queue.Dequeue();

        }
        isProcessing = false;
    }
    public void ClearQueue()
    {
        queue.Clear();
        isProcessing = false;
    }
}