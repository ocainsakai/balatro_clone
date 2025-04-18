using System.Collections.Generic;
using UnityEngine;

namespace Game.CardSystem
{
    [CreateAssetMenu(menuName = ("CardSystem/Card Database"))]

    public class CardDatabase : ScriptableObject
    {
        public List<CardData> cards;
    }

}
