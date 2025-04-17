using Balatro.Cards.UI;
using UnityEngine;


namespace Balatro.Cards.System
{
    public class CardManager : BaseManager
    {
        [Header("Spawn")]
        [SerializeField] CardFactory cardFactory;
        [SerializeField] Transform _cardContainer;

        [SerializeField] public CardDatabase cardDatabase ;
        [SerializeField] CardsRSO handCards;
        [SerializeField] CardsRSO selectedCards;
        public Deck deck = new Deck();
        public DiscardPile discardPile = new DiscardPile();

        int HandCount => handCards.List.Count;
        int HandSize = 8;
        //private void Start()
        //{
        //    Initialize();
        //}
        public override void Initialize()
        {
            base.Initialize();
            deck.Initialize(cardDatabase.database);

        }
        public void DrawCard()
        {
            int amount = HandSize - HandCount;
            for (int i = 0; i < amount; i++)
            {
                if (deck.isEmpty)
                {
                    deck.Initialize(discardPile.GetAllCards());
                    discardPile.Clear();
                }

                var cardData = deck.Draw();   
                UnityEngine.Debug.Log("Card out deck: "+ cardData.name);

                var card = cardFactory.CreateCard(cardData, _cardContainer);
                card.CardView.OnSelected += OnCardSelect;
                handCards.AddCard(card);
            }
            
        }
        public void OnCardSelect(CardView card)
        {
            var data = handCards.List.Find(x => x.CardView == card);
            if (card.isSelected)
            {
                card.Deselect();
                selectedCards.RemoveCard(data);

            }
            else if (selectedCards.List.Count < 5)
            {
                card.Select();
                selectedCards.AddCard(data);
            }
        }
        public void Discard()
        {
            foreach(var card in selectedCards.List)
            {
                handCards.RemoveCard(card);
                discardPile.AddCard(card.data);
            }
            selectedCards.Clear();
            DrawCard();
        }
    }
}

