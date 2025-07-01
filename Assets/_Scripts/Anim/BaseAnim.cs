
using DG.Tweening;
using UnityEngine;

public abstract class BaseAnim : MonoBehaviour
{
    protected EffectManager effectManager => FindFirstObjectByType<EffectManager>();

    [Header("Animation Settings")]
    //[SerializeField] protected bool useAnimation = true;
    [SerializeField] protected float animationDuration = 0.2f;
    [SerializeField] protected Ease animationEase = Ease.InOutQuad;

}