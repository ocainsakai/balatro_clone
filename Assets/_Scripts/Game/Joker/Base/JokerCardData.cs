using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Jokers
{
    [CreateAssetMenu(fileName = "JokerCardData", menuName = "Scriptable Objects/JokerCardData")]

    public class JokerCardData : ScriptableObject
    {
        //public string LogicID;
        public string Name;
        public Sprite Artwork;
        public string Description;

        [SerializeReference, SubclassSelector]
        public List<EffectData> effectsData;

        [SerializeReference, SubclassSelector]
        public List<EffectCondition> activeConditions;
    }

}