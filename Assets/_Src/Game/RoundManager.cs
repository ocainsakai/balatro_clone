using System;
using UniRx;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public ReactiveProperty<RoundState> CurrentState { get; private set; } = new(RoundState.Idle);
    public PlayerManager player;
    

    private void Start()
    {
        StartRound();
    }
    public void StartRound()
    {
        CurrentState.Value = RoundState.Drawing;

        player.DrawCards();

        CurrentState.Value = RoundState.Playing;

        // Có thể chờ input người chơi hoặc thời gian
    }
    public void PlayHand()
    {
        //if (player.CurrentHand.Count == 0) return;

        //CurrentState.Value = RoundState.Scoring;

        //player.RecalculateScore();

        //ShowResult(player.CurrentScoreResult.Value);
    }
    private void Cleanup()
    {
        CurrentState.Value = RoundState.Cleanup;

        //player.ClearHand(); // Tự implement

        Observable.Timer(TimeSpan.FromSeconds(1))
                  .Subscribe(_ => StartRound())
                  .AddTo(this);
    }
}
public enum RoundState
{
    Idle,
    Drawing,
    Playing,
    Scoring,
    Result,
    Cleanup
}
