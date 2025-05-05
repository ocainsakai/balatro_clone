using UnityEngine;
using VContainer;
using UniRx;
using Game.Cards;
using System.Collections;
using Game.Pokers;
namespace Game.System.Score
{
    public class ScoreManagerView : GridViewBase
    {
        [Inject] ScoreManager scoreManager;
        [Inject] CardFactory cardFactory;
        private void Awake()
        {
            scoreManager.OnScore.Subscribe(x => this.AddProcess(OnScoreCard(x.Card)));
            scoreManager.PlayedCards.ObserveAdd().Subscribe(x => this.AddProcess(OnCardAdd(x.Value)));
        }
        private IEnumerator OnScoreCard(Card card)
        {
            var cardView = cardFactory.GetCardFormPool(card);
            cardView.ShowValue();
            yield return new WaitForSeconds(0.2f);
        }
        private IEnumerator OnCardAdd(Card card)
        {
            var cardView = cardFactory.GetCardFormPool(card);
            cardView.transform.SetParent(transform);
            yield return (_layout.RepositionChildren());
        }
    }
}