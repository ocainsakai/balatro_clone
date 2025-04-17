using System.Collections.Generic;


namespace Balatro.Cards.System  
{

    public class DiscardPile
    {
        private List<CardData> discardedCards = new List<CardData>();

        public void AddCard(CardData card)
        {
            discardedCards.Add(card);
        }

        public void Clear()
        {
            discardedCards.Clear();
        }

        public List<CardData> GetAllCards()
        {
            return new List<CardData>(discardedCards);
        }
    }


}
