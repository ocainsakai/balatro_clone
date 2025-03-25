using System.Collections;
using UnityEngine;


public class GameManager : SingletonAbs<GameManager>
{
    [SerializeField] UIManager uiManager;
    [SerializeField] public UIRun uiRun;
    public class Run
    {
        public int round_score;
        public int play_hands;
        public int discards;
        public int money;
        public int ante;
        public int round;
    }
    public Run run { get; private set; }

    void Start()
    {
        InitGame();
    }
    public void InitGame()
    {
        run = new Run()
        {
            round_score = 0,
            play_hands = 4,
            discards = 4,
            money = 0,
            ante = 1,
            round = 1
        };
        uiRun.UpdateRunUI(run);
    }
    public void PlayHand()
    {
        run.play_hands--;
        run.round++;
        uiRun.UpdateRunUI(run);
    }
    public void Discard()
    {
        run.discards--;
        uiRun.UpdateRunUI(run);
    }
    public IEnumerator AddScore(int score)
    {
        yield return (uiRun.AddScore(score));
        run.round_score += score;
    }
}
public enum Phase
{
    Blind = 1,
    Score = 2,
    Shop = 3
}