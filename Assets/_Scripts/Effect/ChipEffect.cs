using Cysharp.Threading.Tasks;
using UnityEngine;

public class ChipEffect : BaseEffect
{
    public async UniTask Add(int amount)
    {
        pokerContext.Chip.Value += amount;
        await effectManager.AddChipEffect(transform, amount);
    }
    public async UniTask Add(Transform transform, int amount)
    {
        pokerContext.Chip.Value += amount;
        await effectManager.AddChipEffect(transform, amount);
    }
}
