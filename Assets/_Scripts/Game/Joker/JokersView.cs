using Game.Cards;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Jokers
{
    public class JokerView : GridViewBase<JokerView>
    {
        [Inject] JokerViewModel viewModel;
        [SerializeField] List<JokerCardData> starting;
        protected override void Awake()
        {
            base.Awake();
            foreach (var item in starting)
            {
                var joker = new JokerCard(item);
                viewModel.Add(joker);
                cardFactory.CreateJoker(joker, transform);
            }
            Repo();
        }
        async void Repo()
        {
            await
            _layout.RepositionChildren();
        }
    }
}