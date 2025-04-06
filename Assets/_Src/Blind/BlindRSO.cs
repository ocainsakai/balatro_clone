using UnityEngine;
using UnityEngine.Events;

namespace Blind
{
    [CreateAssetMenu(menuName = "SO/BlindRuntimeSO")]
    public class BlindRSO : RSO, IBlind
    {
        [SerializeField] IntVariable baseChip;
        [SerializeField] IBlind data;
        public IBlind Data
        {
            get => data;
            set
            {
                this.data = value;
                onBlindChange?.Invoke(data);
            }
        }
        public string Name => data.Name;

        public Sprite Sprite => data.Sprite;

        public int minimunAnte => data.minimunAnte;

        public float scoreMultiple => data?.scoreMultiple ?? 0;

        public int reward => data.reward;
        public long BlindScore => (long) (baseChip.Value * scoreMultiple);
       

        public UnityAction<IBlind> onBlindChange;


        protected override void OnReset()
        {

        }
    }

}
