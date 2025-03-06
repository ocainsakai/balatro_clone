using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public List<BlindSO> blindSO;
    public BlindSO small;
    public BlindSO big;
    public BlindSO boss;

    private void Reset()
    {
        blindSO.AddRange(Resources.LoadAll<BlindSO>("BlindSO"));
    }
    public void Awake()
    {
        Reset();
        UpdateBlindUI();
    }
    public void UpdateBlindUI()
    {
        foreach (Transform item in transform)
        {
            item.GetComponent<BlindUI>().UpdateBlind(small);
        }
    }
    public void NewAnte(int base_score, int ante)
    {
        
    }
    private BlindSO RandomBoss(int ante)
    {
        List<BlindSO> useable = new List<BlindSO>();
        foreach (var item in blindSO)
        {
            if (item.minimun_ante >= ante)
            {
                useable.Add(item);
            }
        }
        int index = UnityEngine.Random.Range(0, useable.Count);
        return useable[index];
    }
}
    