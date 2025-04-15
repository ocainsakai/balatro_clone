using UnityEngine;


namespace Balatro.Cards.System
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] HandData hand;
        public Deck deck;
        public DiscardPile discardPile;
        public CardDatabase cardDatabas => Resources.Load<CardDatabase>(typeof(CardDatabase).ToString());

        int HandCount => hand.cards.Count;
        int HandSize = 8;
        private void Start()
        {
            deck = new Deck();
            discardPile = new DiscardPile();
            deck.Initialize(cardDatabas.database);
            DrawCard();
        }
        public void DrawCard()
        {
            int amount = HandSize - HandCount;
            for (int i = 0; i < amount; i++)
            {
                var cardData = deck.Draw();
                if (cardData != null)
                {
                    hand.AddCard(cardData);
                }
            }
            
        }

        public void DiscardCard(CardData card)
        {
            hand.RemoveCard(card);
            discardPile.AddCard(card);
        }
    }
}

