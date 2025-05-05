using Game.Cards;
using Game.Cards.Decks;
using Game.Player.Hands;
using Game.System.Score;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using VContainer;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] CardView cardPrefab;
        [Inject] HandViewModel hand;
        [Inject] ScoreManager scoreManager;
        [Inject] Deck deck;
        public Subject<Unit> OnDraw = new Subject<Unit>();
        public Subject<Card> OnDiscard = new Subject<Card>();   
        public Subject<Unit> OnPlayed = new Subject<Unit>();


        private List<Card> discardsPile = new();

        private void Awake()
        {
            
        }
        private IEnumerator PlayCard()
        {
            var selected = new List<Card>( hand.GetCardInState(CardState.Selected));
            foreach (Card card in selected)
            {
                hand.Remove(card);  
                scoreManager.Add(card);
            }
            OnPlayed.OnNext(Unit.Default);
            yield return null;
            scoreManager.Score();
        }
        
        private IEnumerator Discard()
        {
            for (int i = hand.Count - 1; i >= 0; i--)
            {
                var card = hand.Cards[i];

                if (card.State.Value == CardState.Selected)
                {
                    hand.Remove(card);
                    discardsPile.Add(card);
                    OnDiscard.OnNext(card);
                }
            }
            yield return DrawHand();
        }
        
        #region Draw
        public IEnumerator DrawHand()
        {
            deck.Shuffle();

            int amount = 8 - hand.Count;
            if (amount > 0)
            {
                yield return DrawCards(amount);
            }
            yield return hand.Sort();
            OnDraw.OnNext(Unit.Default);
        }
        public IEnumerator DrawCards(int amount)
        {

            for (int i = 0; i < amount; i++)
                yield return DrawCard();
        }
        public void ReshuffleDiscardIntoDeck()
        {
            foreach (var card in discardsPile)
                deck.cards.Add(card);
            discardsPile.Clear();
            deck.Shuffle();
        }
        public IEnumerator DrawCard()
        {
            if (deck.cards.Count == 0)
                ReshuffleDiscardIntoDeck();

            var card = deck.cards[0];
            deck.cards.RemoveAt(0);
            hand.Add(card);
            yield return null;
        }
        #endregion
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                StartCoroutine(DrawHand());
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                StartCoroutine(Discard());
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                StartCoroutine(hand.Sort());
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                StartCoroutine(PlayCard());
            }
        }
    }
}