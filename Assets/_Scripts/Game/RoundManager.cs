using Game.Cards;
using UniRx;
using UnityEngine;

namespace Game
{
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] CardView cardPrefab;
        public Subject<Unit> OnDraw = new Subject<Unit>();
        public Subject<Unit> OnDiscard = new Subject<Unit>();   
        public Subject<Unit> OnPlayed = new Subject<Unit>();
        public enum RoundState
        {
            None,
            Select,
            Play,

        }
        public ReactiveProperty<RoundState> roundState = new ReactiveProperty<RoundState>();

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                OnDraw.OnNext(Unit.Default);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                OnDiscard.OnNext(Unit.Default);

            }
            if (Input.GetKeyUp(KeyCode.A))
            {
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                OnPlayed.OnNext(Unit.Default);
            }
        }
    }
}