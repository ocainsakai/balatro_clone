using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    protected PokerContext pokerContext => Resources.Load<PokerContext>("Context/"+typeof(PokerContext));
    protected EffectManager effectManager => FindFirstObjectByType<EffectManager>();

}
