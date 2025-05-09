
using System;
using Game;
using Game.Pokers;

[Serializable]
public abstract class EffectData : IEffect
{
    public abstract void Apply(PokerViewModel system);
}