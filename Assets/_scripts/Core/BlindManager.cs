using System.Collections.Generic;
using UnityEngine;
public class BlindManager : SingletonAbs<BlindManager>
{
    [SerializeField] UIBlind small_blind;
    [SerializeField] UIBlind big_blind;
    [SerializeField] UIBlind boss_blind;

    [SerializeField] BlindSO small_blind_SO;
    [SerializeField] BlindSO big_blind_SO;
    [SerializeField] List<BlindSO> bosses_blind_SO;

    public int anteLevel => GameManager.instance.run.ante;
    public void Initlize()
    {
        small_blind.SetBlind(small_blind_SO, 300);
        big_blind.SetBlind(big_blind_SO, 300);
        boss_blind.SetBlind(RandomBoss(), 300);
        small_blind.InChoosing();
    }
    BlindSO RandomBoss()
    {
        List<BlindSO> canGetBoss = bosses_blind_SO.FindAll(blind => blind.minimunAnte <= anteLevel);
        int k = Random.Range(0, canGetBoss.Count);
        BlindSO boss = canGetBoss[k];
        bosses_blind_SO.Remove(boss);
        return boss;
    }
}
