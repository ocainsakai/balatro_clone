using Cysharp.Threading.Tasks;

public class Joker : JokerCard
{
    MultEffect effect => GetComponent<MultEffect>();
    public override UniTask PostActive()
    {
        return effect.Add(4);
    }
}
