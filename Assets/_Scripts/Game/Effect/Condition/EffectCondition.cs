using System;
using Game.Cards;

[Serializable]
public abstract class EffectCondition
{
    public abstract bool CanApply(ConditionContext context);
}
public class ConditionContext
{
    public CardSuit suitContext;
}
[Serializable]
public class NoCondition : EffectCondition
{
    public override bool CanApply(ConditionContext context)
    {
        return true;
    }
}
[Serializable]
public class SuitCondition : EffectCondition
{

    public CardSuit CardSuit;
    public override bool CanApply(ConditionContext context)
    {
        return CardSuit.Equals(context.suitContext);
    }
}