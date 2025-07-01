using Cysharp.Threading.Tasks;
using UnityEngine;

public class MultEffect : BaseEffect
{
    public async UniTask Add(int amount)
    {
        pokerContext.Mult.Value += amount;
        await effectManager.AddMultEffect(transform, amount);
    }
}
