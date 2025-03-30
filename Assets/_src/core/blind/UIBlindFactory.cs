using UnityEngine;
using UnityEngine.Events;

namespace Balatro.Blind
{
    public class UIBlindFactory : MonoBehaviour
    {
        [SerializeField] public UIBlind small;
        [SerializeField] public UIBlind big;
        [SerializeField] public UIBlind boss;
        public UIBlind currentBlind;
        public void SetSmall(BlindDataSO blindData, int baseChip)
        {
            small.Initlize(blindData, baseChip);
        }
        public void SetBig(BlindDataSO blindData, int baseChip) {
            big.Initlize(blindData, baseChip);

        }
        public void SetBoss(BlindDataSO blindData, int baseChip)
        {
            boss.Initlize(blindData, baseChip);

        }
        public void SelectBlind(int blindIndex, UnityAction action)
        {
            currentBlind = (blindIndex % 3 == 0) ?
                small : (blindIndex % 3 == 1) ?
                big : boss;
            currentBlind.SelectBlind(action);
        }
    }

}
