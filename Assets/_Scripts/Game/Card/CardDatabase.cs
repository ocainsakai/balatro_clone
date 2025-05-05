using System.Collections.Generic;
using UnityEngine;

namespace Game.Cards
{
    [CreateAssetMenu(fileName = "New CardData", menuName = "Card/Card Database")]

    public class CardDatabase : ScriptableObject
    {
        public List<CardData> cardList;
    }
}