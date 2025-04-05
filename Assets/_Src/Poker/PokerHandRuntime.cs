using Card;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Poker
{
    public interface IPokerHand
    {
        string Name { get; }
        int Chip { get; }
        int Mult { get; }
    }
    [CreateAssetMenu(menuName = "SO Variable/PokerHandSO")]

    public class PokerHandRuntime : RuntimeScriptableObject, IPokerHand
    {
        [SerializeField] IntVariable roundScore;
        [SerializeField] PokerHand _pokerHand = new PokerHand();
        [SerializeField] int _level;

        public UnityAction<IPokerHand> OnPokerChanged = delegate {};
        public UnityAction<int> OnLevelChanged = delegate { };

        public PokerHand data
        {
            get => _pokerHand;
            set {
                if (_pokerHand.Equals(value)) return;
                _pokerHand = value;
                OnPokerChanged?.Invoke(value);
            }
        }
        public int Chip {
            get => _pokerHand.Chip;
            set  {
                if (_pokerHand.Chip == value) return;
                _pokerHand.Chip = value;
                OnPokerChanged?.Invoke(this);

            }
        }
        public int Mult
        {
            get => _pokerHand.Mult;
            set
            {
                if (_pokerHand.Mult == value) return;
                _pokerHand.Mult = value;
                OnPokerChanged?.Invoke(this);

            }
        }

        public int Level
        {
            get => _level;
            set
            {
                if (this._level == value) return;
                this._level = value;
                OnLevelChanged?.Invoke(value);
            }
        }

        public string Name
        {
            get => _pokerHand.Name;
            set
            {   if (_pokerHand.Name == value) return;
                _pokerHand.Name = value;
                OnPokerChanged.Invoke(this);
            }
        }
        protected override void OnReset()
        {
            this._pokerHand.Name = "Poker Hand";
            _pokerHand.Chip = 0;
            _pokerHand.Mult = 0;
            OnPokerChanged?.Invoke(this);
        }
        public void Reset() => OnReset();
        public void AddScore()
        {
            int result = Chip * Mult;
            roundScore.Value += result;
        }
    }
}

