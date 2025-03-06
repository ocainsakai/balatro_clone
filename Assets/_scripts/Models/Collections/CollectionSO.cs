using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectionSO", menuName = "Scriptable Objects/CollectionSO")]
public class CollectionSO<T> : ScriptableObject where T : CollectableSO
{
    public List<T> avalableItems => new List<T>(Resources.LoadAll<T>($"{typeof(T)}"));
    public int Count => avalableItems.Count;
    public int Unlock => avalableItems.Count(item => !item.IsLocked);
    
}
