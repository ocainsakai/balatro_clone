using Cysharp.Threading.Tasks;
using UnityEngine;

public class AbstractJoker : JokerCard
{
    MultEffect multEffect => GetComponent<MultEffect>();
    public int MultAmount { get => Manager.jokers.Count * 3; } 
    public override UniTask PostActive()
    {
        return multEffect.Add(MultAmount);
    }
}
