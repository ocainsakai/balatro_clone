using System.Collections.Generic;
using UnityEngine;

namespace Balatro.Ante
{
    public interface IAnteManager
    {
        int CurrentAnte { get; }
        int TotalAntes { get; }
        bool IsLastAnte { get; }

        void StartNewAnte();
        void EndCurrentAnte();
        AnteData GetCurrentAnteData();
    }
    public class AnteData {
        public int RoundNumber { get; set; }
        public int TargetScore { get; set; }
        public float Multiplier { get; set; }
        public List<string> SpecialConditions { get; set; } = new List<string>();
    }

    public class AnteManager : MonoBehaviour, IAnteManager
    {
        //[SerializeField] UIAnteInfo _uiAnteInfo;
        [SerializeField] List<AnteData> _anteConfiguration = new List<AnteData>();

        private int _currentAnteIndex = -1;
        public int CurrentAnte => _currentAnteIndex +1;

        public int TotalAntes => _anteConfiguration.Count;

        public bool IsLastAnte => _currentAnteIndex >= _anteConfiguration.Count -1;

        public void StartNewAnte()
        {
            if (_currentAnteIndex < _anteConfiguration.Count - 1)
            {
                _currentAnteIndex++;
                UpdateAnteUi();
            } else
            {
                // win
            }
        }
        public void EndCurrentAnte()
        {
            throw new System.NotImplementedException();
        }

        public AnteData GetCurrentAnteData()
        {
            throw new System.NotImplementedException();
        }

        private void UpdateAnteUi()
        {
        }
       
    }

}
