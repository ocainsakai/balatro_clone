using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class BlindManager
{
    public ReactiveProperty< BlindViewModel> CurrentBlind = new ReactiveProperty<BlindViewModel>();
    //public ReactiveCollection<BlindData> CurrentAnteBlind = new ReactiveCollection<BlindData>();
    public ReactiveProperty<int> CurrentAnte = new ReactiveProperty<int>(0);
    private BlindData smallData;
    private BlindData bigData;
    private BlindData bossData;
    private List<BlindData> _data = new List<BlindData>();
    private List<BlindData> _appeared = new List<BlindData>();
    private int _blindIndex = 0;
    public int targetScore { get; private set; }
    public BlindManager()
    {
        _data = Resources.LoadAll<BlindData>("Blind").ToList();
        smallData = _data.Find(x => x.Name == "Small Blind");
        bigData = _data.Find(x => x.Name == "Big Blind");
        _appeared.Add(smallData);
        _appeared.Add(bigData);
        CurrentAnte.Subscribe(data => NewAnte(data));
        CurrentBlind.Subscribe(x => SetTargetScore(x.Data));
    }
    public void NextBlind()
    {
        _blindIndex++;
        SetCurrentBlind(_blindIndex);
    }
    private void SetTargetScore(BlindData data)
    {
        targetScore = (int)(GetBaseChip() * data.ScoreMultiple);
    }
    public int GetBaseChip()
    {
        return AnteData.baseChip[CurrentAnte.Value];
    }
    private void NewAnte(int anteLevel)
    {
        bossData = _data.Where(x => x.MinAnte <= anteLevel)
                    .Except(_appeared)
                    .OrderBy(x => Random.value)
                    .FirstOrDefault();
        //CurrentBlind.Value = new BlindViewModel(smallData);
        _blindIndex = 0;
        SetCurrentBlind(_blindIndex);
    }
    private void SetCurrentBlind(int index)
    {
        switch (index)
        {
            case 0:
                CurrentBlind.Value = new BlindViewModel(smallData);
                break;
            case 1:
                CurrentBlind.Value  = new BlindViewModel(bigData);
                break;
            case 2:
                CurrentBlind.Value = new BlindViewModel(bossData); 
                break;
        }
    }
}