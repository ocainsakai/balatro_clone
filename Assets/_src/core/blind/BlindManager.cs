using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Balatro.Blind
{
    public class BlindManager : MonoBehaviour
    {
        [SerializeField] UIBlindFactory factory;

        [SerializeField] BlindDataSO smallBlind;
        [SerializeField] BlindDataSO bigBlind;
        [SerializeField] BlindDataSO bossBlind;
        [SerializeField] List<BlindDataSO> bossBlinds;
        public BlindDataSO currentBlind { get; private set; }

        public event Action OnSelectBlind;
        public event Action NextAnte;

        private List<int> baseChips = new List<int>()
        { 100, 300, 800, 2000, 5000, 11000, 20000, 35000, 50000};
        private int _currentBlindIndex = -1;
        public int _anteLevel = 0;
        private int baseChip =>
           baseChips[Mathf.Clamp(_anteLevel, 0, baseChips.Count)];
        public int blindScore => 
            (int) (currentBlind.scoreMultiplier * baseChip);
        
        public BlindDataSO RandomBoss(int anteLevel) {
            var avaialbeBoss = bossBlinds.Where(x => x.minAnte <= anteLevel).ToList();
            return avaialbeBoss[UnityEngine.Random.Range(0, avaialbeBoss.Count)];
        }

        public void Defeat()
        {
            factory.currentBlind.Defeat();
        }
        public void NewAnte()
        {
            _anteLevel++;
            NextAnte?.Invoke();
            //int baseChip = Mathf.Clamp(_anteLevel, 0, baseChips.Count);
            bossBlind = RandomBoss(_anteLevel);
            factory.SetSmall(smallBlind, baseChip);
            factory.SetBig(bigBlind, baseChip);
            factory.SetBoss(bossBlind, baseChip);
            //factory.Unlock();
        }
        public void StartPhase()
        {
            _currentBlindIndex++;
            if (_currentBlindIndex % 3 == 0)
            {
                NewAnte();
                currentBlind = smallBlind;
            } else if (_currentBlindIndex % 3 == 1)
            {
                currentBlind = bigBlind;
            } else
            {
                currentBlind = bossBlind;
            }
            factory.SelectBlind(_currentBlindIndex,
                () => OnSelectBlind?.Invoke());
        }
    }
}

