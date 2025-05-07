using Game.Cards;
using Game.Cards.Decks;
using Game.Player.Hands;
using Game.System.Score;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game
{
    public class PlayManager : MonoBehaviour
    {
        [SerializeField] CardView cardPrefab;
        [Inject] BlindManager blindManager;
        public Subject<Unit> OnDraw = new Subject<Unit>();
        public Subject<Unit> OnDiscard = new Subject<Unit>();   
        public Subject<Unit> OnPlayed = new Subject<Unit>();


        private List<Card> discardsPile = new();

        private void Awake()
        {
            blindManager.CurrentAnte.Value++;
        }
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