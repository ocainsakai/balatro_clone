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
        public Trigger trigger;
    }
    public enum Trigger
    {
        Independent,
        OnScore,
    }
}