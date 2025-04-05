using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlindManager : MonoBehaviour
{
    [SerializeField] List<BlindSO> allBlinds;
    [SerializeField] BlindRuntimeSO smallBlind;
    [SerializeField] BlindRuntimeSO bigBlind;
    [SerializeField] BlindRuntimeSO bossBlind;

    private void Start()
    {
        smallBlind.BaseChips = 300;
        bigBlind.BaseChips = 300;
        bossBlind.Data = allBlinds[0];
        bossBlind.BaseChips = 300;
    }
}
