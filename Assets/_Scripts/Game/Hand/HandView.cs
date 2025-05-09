using Game.Cards;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Player.Hands
{

    public  class HandView : GridViewBase<CardView>
    {
        protected HandViewModel hand;
        [Inject]
        public void Construct(HandViewModel hand)
        {
            this._cards = hand;
            this.hand = hand;
        }
    }
}