using System;
using Game.Pokers;
using UnityEngine;

namespace Game.Jokers
{
    [Serializable]
    public  class EffectPlusChip : EffectData
    {
        public int Chip;

        public override void Apply(PokerViewModel system)
        {
            system.Chip.Value += Chip;
        }
    }
}