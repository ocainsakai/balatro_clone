
using System.Collections.Generic;
using UnityEngine;


public abstract class BasePhaseUI : MonoBehaviour, IPhaseUI 
{
    public readonly static List<BasePhaseUI> Instances = new List<BasePhaseUI>();
    //public abstract Type Phase { get; }
    private void OnEnable()
    {
        Instances.Add(this);
    }
    private void OnDisable()
    {
        Instances.Remove(this);
    }
    public void Turn(bool state)
    {
        Debug.Log(gameObject.name + " " + state);
        if (state) Show();
        else Hide();
    }
    public static T GetPhaseUI<T>() where T : BasePhaseUI
    {
        return Instances.Find(x => x is T) as T;
    }
    public abstract void Hide();

    public abstract void Show();
}
