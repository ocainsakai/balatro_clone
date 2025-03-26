using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BalatroClone.Cards;
namespace BalatroClone.Gameplay
{
    public class DeckManager : SingletonAbs<DeckManager>, ICardFactory
    {
        private List<CardSO> defaulsCards
            => Resources.LoadAll<CardSO>("CardSO").ToList();

        [SerializeField] private GameObject uiCardPrf;

        private List<CardSO> playingCards;
        //private List<Card> deck = new List<Card>();
        //private List<Card> hand = new List<Card>();
        //private List<Card> discardPile = new List<Card>();

        public void Initialize(List<CardSO> initDeck)
        {
            foreach (var cardSO in initDeck)
            {
                //deck.Add(CreateCard(cardSO));
            }
            Shuffle();
        }

        public void Shuffle()
        {
            // Logic trộn bài
        }

        public void DrawHand(int handSize)
        {
            // Logic rút bài
        }
        //[SerializeField] UIHandManager uiDeck;
        //Card CreateCard(CardSO card)
        //{
        //    Card newCard = Instantiate(uiCardPrf).GetComponent<Card>();
        //    deck.Add(newCard);
        //    newCard.Initlize(card);
        //    newCard.transform.SetParent(transform);
        //    //newCard.transform.SetPositionAndRotation(deckPosition.position, Quaternion.identity);
        //    newCard.transform.localScale = Vector3.one;
        //    newCard.gameObject.SetActive(false);
        //    return newCard;
        //}

        public void Start()
        {
            playingCards = new List<CardSO>(defaulsCards);
        }

        public Card CreateCard(CardSO cardSO)
        {
            throw new System.NotImplementedException();
        }
    }
}

