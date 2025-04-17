using Balatro.Cards;
using Balatro.Cards.UI;
using System;
using UnityEngine;

namespace Balatro.Cards.System
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private Transform _defaultCardPrf;
        
        public Card CreateCard(CardData data, Action<CardView> OnCardClicked)
        {
            // Later this could be extended to choose prefab based on card type
            var instance = Instantiate(_defaultCardPrf, transform);

            var card = instance.GetComponent<Card>();

            if (card == null)
            {
                Debug.LogError("CardPrefab missing CardView component.");
                return null;
            }

            card.Setup(data, OnCardClicked);
            return card;
        }

    }

}
