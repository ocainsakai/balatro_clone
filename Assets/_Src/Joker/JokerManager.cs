
using System.Collections.Generic;
using UnityEngine;
namespace Joker
{
    public class JokerManager : MonoBehaviour
    {
        List<JokerCard> jokers;
        [SerializeField] Transform jokerContainer;
        int MaxJoker = 5;
        public bool AddJoker(JokerCard card)
        {
            if (jokers.Count == MaxJoker) return false;
            jokers.Add(card);
            return true;
        }
    }
}

