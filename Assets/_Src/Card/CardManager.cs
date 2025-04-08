using Balatro.Poker;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

namespace Balatro.Card
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] Transform cardContainer;
        [SerializeField] GameObject cardPrf;
        [SerializeField] IntVariable round;
        List<ICard> playingDeck = new List<ICard>();
        List<ICard> discardPile = new List<ICard>();
        List<PlayingCard> hand = new List<PlayingCard>();
        List<PlayingCard> selectedCards => hand.FindAll(x => x.IsSelected);
        //List<ICard> selectedCards => hand.Where(x => x.IsSelected).ToList();
        public int MaxHandSize = 8;
        public int HandCount => hand.Count;
        public int SelectedCount =>selectedCards.Count;
        void Awake()
        {
            Sort = () =>
            {
                hand = hand.OrderBy(x => x.Rank).ThenBy(x => x.Suit).ToList();
                SyncHandWithContainer();
            };
        }
        public void SetPlayingDeck(IEnumerable<ICard> cards)
        {
 
            this.playingDeck = cards.ToList();
            Shuffe();
        }
        public void NewRound()
        {
            round.Value++;
            Draw();
        }
        private void Shuffe()
        {
            //if (!canShuffe) return;
            for (int i = playingDeck.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                (playingDeck[i], playingDeck[randomIndex]) = (playingDeck[randomIndex], playingDeck[i]);
            }
        }
        private void Draw()
        {
            int amount = MaxHandSize - hand.Count;
            if (playingDeck.Count < amount)
            {
                playingDeck.AddRange(discardPile);
                discardPile.Clear();
                Shuffe();
            }
            for (int i = 0; i < amount; i++)
            {
                var card = playingDeck[i];


                hand.Add(CreatCard(card));
            }
            playingDeck.RemoveRange(0,amount);
            Sort();
        }
        PlayingCard CreatCard(ICard card)
        {
            var newCardObject = Instantiate(cardPrf, cardContainer);
            var newCard = newCardObject.GetComponent<PlayingCard>();
            newCard.transform.SetParent(cardContainer.transform);
            newCardObject.transform.localScale = Vector3.one;
            newCard.Initialize(card, this);
            return newCard;
        }
        public void Play()
        {
            var result = PokerEvaluator.Evaluator(selectedCards.ConvertAll(x => (ICard)x));
            if (result != null)
            {
                Debug.Log(result.PokerHand.Name);
            }
            // game state : decide
            Discard();
        }
        UnityAction Sort = () => {};

        public void SortByRank()
        {
            Sort = () =>
            {
                hand = hand.OrderBy(x => x.Rank).ThenBy(x => x.Suit).ToList();
                SyncHandWithContainer();
            };
            Sort();
        }
        public void SortBySuit()
        {
            Sort = () =>
            {
                hand = hand.OrderBy(x => x.Suit).ThenBy(x => x.Rank).ToList();
                SyncHandWithContainer();
            };
            Sort();
        }
        public void Discard()
        {
            selectedCards.ForEach(x => { x.OnRemove(); });
            discardPile.AddRange(selectedCards);
            hand.RemoveAll(x => selectedCards.Contains(x));

            NewRound();
        }
        void SyncHandWithContainer()
        {

            float cardWidth = cardPrf.GetComponent<RectTransform>().rect.width;
            float spacing = cardWidth + 15f;
            float totalWidth = (hand.Count - 1) * spacing;
            float startX = -totalWidth / 2; 
            for (int i = 0; i < hand.Count; i++)
            {
                var card = hand[i];
                float targetX = startX + i * spacing;
                Vector2 targetPos = new Vector2(targetX, cardContainer.GetComponent<RectTransform>().anchoredPosition.y);
                card.GetComponent<RectTransform>().DOAnchorPos(targetPos, 0.3f).SetEase(Ease.OutQuad);
                card.OnUnselect();
            }
        }
    }

}
