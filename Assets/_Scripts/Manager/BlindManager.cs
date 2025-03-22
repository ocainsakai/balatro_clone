using System.Collections.Generic;
using UnityEngine;


public class BlindManager : SingletonAbstract<BlindManager>
{
    [SerializeField] public List<Blind> bosses;
    [SerializeField] public Blind small;
    [SerializeField] public Blind big;
    
    [SerializeField] private UIBlind ui_small;
    [SerializeField] private UIBlind ui_big;
    [SerializeField] private UIBlind ui_boss;

    public List<Blind> bossList;
    public Blind currentBoss;
    public Blind currentBlind { get; private set; }
    public int currentBlindScore {  get; private set; } 

    public int anteLevel;

    
    private List<int> baseChips = new List<int>() {100, 100, 300, 800, 2000, 5000, 11000, 20000, 35000, 50000 };

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    {
        currentBlindScore = 0;
        anteLevel = 0;
        bossList.AddRange(bosses);
        SetNextAnte();
    }
    public void SetNextAnte()
    {
        anteLevel++;
        currentBoss = RandomBoss();

        ui_small.Initlize(small, baseChips[anteLevel+1]);
        ui_big.Initlize(big, baseChips[anteLevel+1]);
        ui_boss.Initlize(currentBoss, baseChips[anteLevel + 1]);

        ui_small.UnLock();
    }
    public void SetBlind(Blind blind)
    {
        this.currentBlind = blind;
        this.currentBlindScore = (int) (blind.score_multiple * baseChips[anteLevel+1]);
    }
    public void Defeat()
    {
        if (currentBlind == small)
        {
            ui_small.Defeated();
            ui_big.UnLock();
        }
        else if (currentBlind == big)
        {
            ui_big.Defeated();
            ui_boss.UnLock();
        } else if (currentBlind == currentBoss)
        {
            ui_boss.Defeated();
            SetNextAnte();
        }
    }

    Blind RandomBoss()
    { 
        List<Blind> canGetBoss = bossList.FindAll(blind => blind.minAnte <= anteLevel);
        int k = Random.Range(0, canGetBoss.Count);
        Blind boss = canGetBoss[k];
        bossList.Remove(boss);
        return boss;
    }
    public bool IsWin(int score)
    {
        return score >= currentBlindScore;
    }
}

