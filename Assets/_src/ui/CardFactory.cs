using Balatro.Core.Card;
using UnityEngine;

namespace Balatro.UI.Cards
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform cardContainer;


        public CardView CreateCard(CardDataSO cardData)
        {
            // Instantiate prefab
            GameObject newCardObject = Instantiate(cardPrefab, cardContainer);

            // Lấy component CardView
            CardView cardView = newCardObject.GetComponent<CardView>();
            //cardView.CardViewClicked += OnCardViewClicked;

            // Thiết lập dữ liệu
            cardView.SetCardData(cardData);

            return cardView;
        }

        //private void OnCardViewClicked(CardView obj)
        //{
        //    throw new System.NotImplementedException();
        //}

        // Xóa tất cả các thẻ
        public void ClearAllCards()
        {
            foreach (Transform child in cardContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}