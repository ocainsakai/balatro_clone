using Game.Pokers;
using Game.System.Score;
using System.Collections;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Jokers
{
    public class JokerViewModel
    {
        private PokerViewModel pokerViewModel;
        private ReactiveCollection<JokerCard> _jokers = new ReactiveCollection<JokerCard>();
        public IReadOnlyReactiveCollection<JokerCard> Jokers => _jokers;

        [Inject] 
        public JokerViewModel(ScoreManager scoreManager, PokerViewModel pokerViewModel)
        {
            this.pokerViewModel = pokerViewModel;
            //scoreManager.OnScore.Subscribe(x => OnScoreHandle(x));
            //scoreManager.PostScore.Subscribe(x => PostScoreHandle(x));
        }
        private void PostScoreHandle(PostScoreContext context)
        {
            
        }
        private void OnScoreHandle(OnScoreContext context)
        {
            foreach (var item in _jokers)
            {
               

            }
        }
        public void Add(JokerCard card)
        {
            _jokers.Add(card);
        }
    }
}