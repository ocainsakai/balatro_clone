
using UnityEngine;

namespace Balatro.Cards
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform cardContainer;

        public Card CreateCard(CardDataSO cardData)
        {
            GameObject newCardObject = Instantiate(cardPrefab, cardContainer);
            Card card = newCardObject.GetComponent<Card>();
            card.SetUI(cardData);

            return card;
        }
        public void ClearAllCards()
        {
            foreach (Transform child in cardContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}