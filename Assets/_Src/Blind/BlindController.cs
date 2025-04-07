using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Blind
{
    public class BlindController : MonoBehaviour
    {
        [SerializeField] GameStateEvent gameStateEvent;
        [SerializeField] IntVariable money;
        [SerializeField] IntVariable baseChip;
        [SerializeField] BlindRSO blindRSO;

        [SerializeField] BlindUnit small;
        [SerializeField] BlindUnit big;
        [SerializeField] BlindUnit boss;

        [SerializeField] BlindSO smallSO;
        [SerializeField] BlindSO bigSO;
        BlindSO bossSO;    
        [SerializeField] List<BlindSO> allBlinds;
        List<BlindSO> existBlinds = new List<BlindSO>();

        public long BlindScore => blindRSO.BlindScore;
        int ante;
        int blindIndex;
        public void Awake()
        {
            blindRSO.Data = default;
            ante = 1;
            small.OnSelect += HandleSelect;
            big.OnSelect += HandleSelect;
            boss.OnSelect += HandleSelect;
            NewAnte(ante);
        }
        public void NewAnte(int ante)
        {
            blindIndex = 0;
            bossSO = RandomBoss(ante);
            small.Data = smallSO;
            big.Data = bigSO;
            boss.Data = bossSO;

            baseChip.Value = (int) AnteDatabase.baseChips_1[ante];

            small.state = BlindState.Ready;
            big.state = BlindState.Locked;
            boss.state = BlindState.Locked;
        }
        void HandleSelect(BlindUnit blind)
        {
            blindRSO.Data = blind.Data;
            gameStateEvent.Raise(Core.GameState.Playing);
        }
        BlindSO RandomBoss(int ante)
        {
            var validsboss = allBlinds.Except(existBlinds).Where(x => x.minimunAnte <= ante).ToList();
            var boss = validsboss[Random.Range(0, validsboss.Count)];
            existBlinds.Add(boss);
            return boss;
        }

        public void Defeat()
        {
            blindRSO.Data = default;
            switch (blindIndex)
            {
                case 0:
                    small.state = BlindState.Defeated;
                    money.Value += small.Data.reward;
                    big.state = BlindState.Ready;
                    blindIndex++;
                    break;
                case 1:
                    big.state = BlindState.Defeated;
                    money.Value += big.Data.reward;
                    boss.state = BlindState.Ready;
                    blindIndex++;
                    break;
                case 2:
                    boss.state = BlindState.Defeated;
                    money.Value += boss.Data.reward;
                    NewAnte(++ante);
                    break;
            }
        }
    }

}
