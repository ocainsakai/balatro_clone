using Game.Cards;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Jokers
{
    public class JokerView : GridViewBase
    {
        [Inject] CardFactory cardFactory;
        [Inject] JokerViewModel viewModel;
        [SerializeField] List<JokerCardData> starting;
        private void Awake()
        {
            foreach (var item in starting)
            {
                var logic = JokerLogicFactory.CreateLogic(item.LogicID);
                var joker = new JokerCard(item, logic);
                viewModel.Add(joker);
                cardFactory.CreateJoker(joker, transform);
            }
            StartCoroutine( _layout.RepositionChildren());
        }
    }
}