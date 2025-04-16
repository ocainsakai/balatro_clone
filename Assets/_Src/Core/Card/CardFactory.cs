using Balatro.Cards.CardsRuntime;
using UnityEngine;

namespace Balatro.Cards.System
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private Transform _defaultCardPrf;
        
        public Card CreateCard(CardData data, Transform _cardContainer)
        {
            // Later this could be extended to choose prefab based on card type
            var instance = Instantiate(_defaultCardPrf, _cardContainer);

            var card = instance.GetComponent<Card>();

            if (card == null)
            {
                Debug.LogError("CardPrefab missing CardView component.");
                return null;
            }

            card.Setup(data);
            return card;
        }
    }

}
