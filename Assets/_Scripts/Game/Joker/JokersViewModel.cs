using Game.Cards;
using Game.Pokers;
using Game.System.Score;
using System.Collections;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace Game.Jokers
{
    public class JokerViewModel
    {
        private PokerViewModel pokerViewModel;
        private ScoreManager scoreManager; 
        private ReactiveCollection<JokerCard> _jokers = new ReactiveCollection<JokerCard>();
        public IReadOnlyReactiveCollection<JokerCard> Jokers => _jokers;

        [Inject] 
        public JokerViewModel(ScoreManager scoreManager, PokerViewModel pokerViewModel)
        {
            this.pokerViewModel = pokerViewModel;
            this.scoreManager = scoreManager;
            scoreManager.OnScore.Subscribe(x => OnScoreHandle(x));
            scoreManager.PostScore.Subscribe(x => PostScoreHandle(x));
        }
        private void PostScoreHandle(PostScoreContext context)
        {
            Debug.Log("trigger post score");
            foreach (var item in _jokers)
            {
                if (item.jokerLogic is IPostScoreJoker)
                {
                    var trigger = item.jokerLogic as IPostScoreJoker;
                    if ( trigger.Condition(context))
                    {
                        pokerViewModel.ApplyEffect(item.jokerLogic);
                    }
                }
            }
            // temp
            //
            scoreManager.ScoreCalculate();
        }
        private void OnScoreHandle(OnScoreContext context)
        {
            foreach (var item in _jokers)
            {
                if (item.jokerLogic is IOnScoreJoker)
                {
                    var trigger = item.jokerLogic as IOnScoreJoker;
                    if (trigger.Condition(context))
                    {
                        pokerViewModel.ApplyEffect(item.jokerLogic);
                    }
                }

            }
        }
        public void Add(JokerCard card)
        {
            _jokers.Add(card);
        }
    }
}