
using UnityEngine;


namespace Card
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] GameObject cardPrf;
        [SerializeField] CardManager cardContainer;
        public void CreatCard(IStandardCard card)
        {
            StandardCard newCard = Instantiate(cardPrf).GetComponent<StandardCard>();
            newCard.SetInit(card, cardContainer);
            cardContainer.AddHand(newCard);
        }
    }
}

