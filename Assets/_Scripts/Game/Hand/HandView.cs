using DG.Tweening;
using Game.Cards;
using System.Collections;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Player.Hands
{

    public  class HandView : GridViewBase
    {
        [Inject] CardFactory _cardFactory;
        [Inject] HandViewModel hand;
        [Inject] GameManager gameManager;
        private void Awake()
        {
            hand.Cards.ObserveAdd().Subscribe(x => this.AddProcess(OnCardDrawn(x))).AddTo(this);
            hand.Cards.ObserveMove().Subscribe(x => this.AddProcess(OnCardMoved(x))).AddTo(this);
            hand.Cards.ObserveRemove().Subscribe(x => this.AddProcess(_layout.RepositionChildren()));
            gameManager.OnDiscard.Subscribe(x => this.AddProcess(OnCardDiscarded(x))).AddTo(this);
        }
        

        private IEnumerator OnCardMoved(CollectionMoveEvent<Card> moveEvent)
        {
            Transform card = _layout.transform.GetChild(moveEvent.OldIndex);
            card.SetSiblingIndex(moveEvent.NewIndex);
            StartCoroutine( _layout.RepositionChildren());
            yield return null;

        }
        private IEnumerator OnCardDrawn(CollectionAddEvent<Card> cardEvent)
        {
            var cardData = cardEvent.Value;
            _cardFactory.CreateCard(cardData, transform);
            yield return _layout.RepositionChildren();
        }
        private IEnumerator OnCardDiscarded(Card card)
        {
            _cardFactory.ReturnCardToPool(card);
            yield return _layout.RepositionChildren();
        }

    }
}