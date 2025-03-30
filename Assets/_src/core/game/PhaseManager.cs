
using System;
using UnityEngine;

namespace Balatro.Core
{
    public class PhaseManager : MonoBehaviour
    {
        
        public event Action StartSelectPhase;
        public event Action StartPlaying;
        public event Action StartShopping;
        public event Action OnGameover;
        public void ChangePhase(Phase newPhase)
        {
            switch (newPhase)
            {
                case Phase.SelectPhase:
                    StartSelectPhase?.Invoke();
                    break;
                case Phase.PlayPhase:
                    StartPlaying?.Invoke();
                    break;
                case Phase.ShopPhase:
                    StartShopping?.Invoke();
                    break;
                case Phase.Gameover:
                    OnGameover?.Invoke();
                    break;

            }
        }
    }
}

