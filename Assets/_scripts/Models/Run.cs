using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Run
{
    // Các thuộc tính cơ bản
    public StakeDifficult stakeDifficulty;
    public DeckSO deck;
    public int round;
    public int ante;
    public int money;

    // Thêm thuộc tính cho gameplay
    public List<CardSO> playingDeck = new List<CardSO>();
    public List<CardSO> discardPile = new List<CardSO>();
    public List<CardSO> handCards = new List<CardSO>();
    public List<CardSO> jokers = new List<CardSO>();

    // Thông tin về hands đã chơi
    public int handsPlayed;
    public int totalScore;
    public float scoreMultiplier = 1.0f;

    // Trạng thái game
    public bool isGameOver;
    public bool isBigWin;

    // Lưu kết quả các round
    public Dictionary<int, int> roundScores = new Dictionary<int, int>();

    // Constructor
    public Run(StakeDifficult stake, DeckSO selectedDeck, int startingMoney = 0)
    {
        stakeDifficulty = stake;
        deck = selectedDeck;
        round = 1;
        ante = CalculateStartingAnte(stake);
        money = startingMoney;

        // Khởi tạo bộ bài chơi từ deck đã chọn
        InitializePlayingDeck();
    }

    // Khởi tạo bộ bài
    private void InitializePlayingDeck()
    {
        playingDeck = deck.defaultCards;
    }

    // Tính toán ante ban đầu dựa trên mức độ khó
    private int CalculateStartingAnte(StakeDifficult stake)
    {
        switch (stake)
        {
            case StakeDifficult.Low:
                return 5;
            case StakeDifficult.Medium:
                return 10;
            case StakeDifficult.High:
                return 20;
            case StakeDifficult.VeryHigh:
                return 30;
            default:
                return 5;
        }
    }

    // Phương thức để bắt đầu round mới
    public void StartNewRound()
    {
        round++;
        ante = CalculateAnte();

        // Logic để bắt đầu vòng mới
    }

    // Tính toán ante cho round hiện tại
    private int CalculateAnte()
    {
        // Trong Balatro, ante thường tăng theo round
        return ante + (int)(ante * 0.5f);
    }

    // Phương thức để rút bài
    public CardSO DrawCard()
    {
        if (playingDeck.Count == 0)
        {
            ShuffleDiscardPile();
        }

        if (playingDeck.Count > 0)
        {
            CardSO drawnCard = playingDeck[0];
            playingDeck.RemoveAt(0);
            return drawnCard;
        }

        return null; // Không còn bài để rút
    }

    // Trộn lại bộ bài từ discard pile
    private void ShuffleDiscardPile()
    {
        playingDeck.AddRange(discardPile);
        discardPile.Clear();

        // Trộn bài
        System.Random rng = new System.Random();
        int n = playingDeck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CardSO temp = playingDeck[k];
            playingDeck[k] = playingDeck[n];
            playingDeck[n] = temp;
        }
    }

    // Thêm phương thức tính điểm
    public int CalculateHandScore(List<CardSO> hand)
    {
        // Logic tính điểm cho một hand trong Balatro
        // Điểm cơ bản + hiệu ứng từ Jokers
        return 0; // Thay với logic tính điểm thật
    }

    // Cập nhật tiền sau mỗi lượt
    public void UpdateMoney(int scoreEarned)
    {
        money += scoreEarned;
        totalScore += scoreEarned;
        roundScores[round] = scoreEarned;
    }

    // Lưu game
    public void SaveRun()
    {
        // Logic lưu trạng thái game
    }
}

// Enum cho mức độ khó
public enum StakeDifficult
{
    Low,
    Medium,
    High,
    VeryHigh
}