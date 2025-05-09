using System;
using System.Collections.Generic;
using System.Linq;
using Game.Cards;

[Serializable]
public abstract class EffectCondition
{
    public abstract bool CanApply(ConditionContext context);
}
public class ConditionContext
{
    public SerializableGuid CardID;
    public CardSuit suitContext;
    public CardRank rankContext;
    public List< IGrouping<CardRank, CardRank>> rankGroup;
    public List<IGrouping<CardSuit, CardSuit>> suitGroup;
    public ConditionContext (SerializableGuid cardID)
    {
        CardID = cardID;
    }
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
[Serializable]
public class RankCondition : EffectCondition
{
    public CardRank CardRank;
    public override bool CanApply(ConditionContext context)
    {
        return this.CardRank == context.rankContext;
    }
}


[Serializable] 
public class PairCondition : EffectCondition
{

    public override bool CanApply(ConditionContext context)
    {
        return context.rankGroup.Where(x => x.Count() == 2).Count() == 1;
    }
}
[Serializable]
public class HasPairCondition : EffectCondition
{
     
    public override bool CanApply(ConditionContext context)
    {
        return context.rankGroup[0].Count() >= 2;
    }
}