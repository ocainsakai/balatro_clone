
using System.Collections.Generic;
using UnityEngine;

namespace Balatro.Cards
{
    [CreateAssetMenu(fileName = "CardDatabase", menuName = "Scriptable Objects/CardDatabase")]
    public class CardDatabase : ScriptableObject
    {
        public List<CardData> database;
    }
}

