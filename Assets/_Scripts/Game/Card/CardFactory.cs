using Game.Jokers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cards
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField] private CardView cardPrefab;
        [SerializeField] private JokerCardView jokerPrefab;
        public List<CardView> pooling = new List<CardView>();
        public JokerCardView CreateJoker(JokerCard joker, Transform parent)
        {
            var jokerView = Instantiate(jokerPrefab, transform.position, Quaternion.identity, parent);
            jokerView.SetData(joker);
            return jokerView;
        }
        public CardView CreateCard(Card card, Transform parent)
        {
            var cardView = GetCardFormPool(card);
            if (cardView == null)
            {
                cardView = Instantiate(cardPrefab, transform.position,Quaternion.identity,parent);
            } else
            {
                cardView.transform.parent = parent; 
                cardView.gameObject.SetActive(true);
            }
            cardView.SetData(card);
            pooling.Add(cardView);
            return cardView;
        }
        public void ReturnCardToPool(Card card)
        {
            var cardView = GetCardFormPool(card);
            if (cardView != null)
            {
                cardView.transform.SetParent(transform);
                cardView.gameObject.SetActive(false);
            }
        }
        public CardView GetCardFormPool(Card card)
        {
            foreach (var item in pooling)
            {
                if (item.GetCard().Data == card.Data)
                    return item;
            }
            return null;
        }
    }
}