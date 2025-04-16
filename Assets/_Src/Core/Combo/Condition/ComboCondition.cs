using Balatro.Cards;

using System.Collections.Generic;

using UnityEngine;


namespace Balatro.Combo.Logic
{
    public abstract class ComboCondition : ScriptableObject
    {
        public abstract bool IsSatisfied(List<CardData> cards, out List<CardData> matchedCards);
    }

    
   
}
