using System;

//[SubclassSelector]
public interface IPlayEffectCommand 
{
    public void Execute(object input, params object[] output);
}
public enum EffectType
{
    AddChip,
    AddMultiplier,
    AddMoney,
    MultiplyHandScore,
    AddCardDraw,
    AddDiscard,
    ExtraJokerSlot,
    SpecialRule, 
}
