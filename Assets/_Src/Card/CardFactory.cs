
using UnityEngine;


namespace Card
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] GameObject cardPrf;
        [SerializeField] Transform cardContainer;
        public StandardCard CreatCard()
        {
            var newCard = Instantiate(cardPrf).GetComponent<StandardCard>();
            newCard.transform.SetParent(transform);
            return newCard;
        }
    }
}

