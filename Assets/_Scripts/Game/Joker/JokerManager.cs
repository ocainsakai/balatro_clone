using System.Collections.Generic;
using Game.Pokers;
using UnityEngine;
using VContainer;
using UniRx;
using Cysharp.Threading.Tasks;
using Game.Cards;
using System.Threading.Tasks;

namespace Game.Jokers
{
    public class JokerManager : GridView
    {
        [SerializeField] List<JokerCardData> jokers;
        private JokerCollection _joker = new JokerCollection();
        [Inject] PokerViewModel pokerViewModel;
        [Inject] ScoreManager scoreManager;
        protected virtual void Awake()
        {
            _joker.Items.ObserveAdd().Subscribe(x =>
            {
                OnItemAdd(x.Value);
            });
            scoreManager.OnScore.Subscribe( async x => await OnScoreHandler(x));
        }

        private async UniTask OnScoreHandler(Card arg)
        {
            foreach (var item in _joker.Items)
            {
            }
            await Task.Yield();
        }

        private void Start()
        {
            foreach (var joker in jokers)
            {
                _joker.Add(new JokerCard(joker));
            }
        }
        private void OnItemAdd(JokerCard jokerCard)
        {
            JokerCardView.Create(jokerCard, transform);
        }
    }
}