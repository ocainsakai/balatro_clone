using System;
using Game.Pokers;
using UnityEngine;

namespace Game.Jokers
{
    [Serializable]
    public class EffectPlusMult : EffectData
    {
        public int Mult;
        public override void Apply(PokerViewModel system)
        {
            system.Mult.Value += Mult;
        }
    }
}