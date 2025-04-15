using Balatro.Cards.CardsRuntime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Balatro.Cards
{
    [CreateAssetMenu(fileName = "HandData", menuName = "Scriptable Objects/HandData")]
    public class HandData : RSO
    {
        public List<CardData> cards;
        public event Action OnUpdated;
        public event Action<CardData> OnAdded;
        public event Action<CardData> OnRemoved;
        public void AddCard(CardData card)
        {
            cards.Add(card);
            OnAdded?.Invoke(card);
            OnUpdated?.Invoke();
        }

        public void RemoveCard(CardData card)
        {
            cards.Remove(card);
            OnRemoved?.Invoke(card);
            OnUpdated?.Invoke();

        }

        public void Clear()
        {
            cards.Clear();
            OnUpdated?.Invoke();
        }

        protected override void OnReset()
        {
            cards = new List<CardData>();
            OnUpdated?.Invoke();
        }
    }

}

