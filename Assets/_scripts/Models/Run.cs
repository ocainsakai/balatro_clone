using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

[System.Serializable]
public class Run
{
    private AnteSO anteList => Resources.Load<AnteSO>(typeof(AnteSO).ToString());
    // Các thuộc tính cơ bản
    public StakeDifficult stakeDifficulty;
    public DeckSO deck;
    public int round { get; private set; }
    public int anteLevel { get; private set; }
    public int money { get; private set; }
    public int baseChips => GetBaseChips();
    // Thêm thuộc tính cho gameplay
    public List<CardSO> playingDeck = new List<CardSO>();
    public List<CardSO> choosingCards = new List<CardSO>();
    public List<CardSO> handCards = new List<CardSO>();
    public List<CardSO> playedCards = new List<CardSO>();
    public List<CardSO> jokers = new List<CardSO>();

    //public event Action<CardSO> onChoosing;
    // Thông tin về hands đã chơi
    public int handSize = 8;
    public int handCount = 4;
    public int discardsCount = 4;
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
        round = 0;
        anteLevel = 0;
        money = startingMoney;

        // Khởi tạo bộ bài chơi từ deck đã chọn
        InitializePlayingDeck();
    }

    // Khởi tạo bộ bài
    private void InitializePlayingDeck()
    {
        playingDeck = new List<CardSO>(deck.defaultCards);
    }

    // Tính toán ante ban đầu dựa trên mức độ khó
    public int GetBaseChips()
    {
        Ante ante = anteList.antes[anteLevel + 1];
        switch (stakeDifficulty)
        {
            case StakeDifficult.white:
            case StakeDifficult.red:
                return ante.baseChipsRequirement;
            case StakeDifficult.green:
            case StakeDifficult.black:
            case StakeDifficult.blue:
                return ante.baseChips_GreenOrHigher;
            case StakeDifficult.purple:
            case StakeDifficult.orange:
            case StakeDifficult.gold:
                return ante.baseChips_PurpleOrHigher;
        }
        return 0;
    }
    private void Draws()
    {
        Shuffe();
        for (int i = handCards.Count; i < handSize; i++)
        {
            CardSO newCard = DrawCard();
            handCards.Add(newCard);
            UIManager.Instance.DrawHand(newCard);
        }
    }
    // Phương thức để b{ắt đầu round mới
    public void NextRound()
    {
        round++;
        Draws();
        UIManager.Instance.runUI.UpdateRun(this);
    }
    public void NextAnte()
    {
        anteLevel++;
        totalScore = 0;
        //NextRound();
        //round++;Nex
        BlindManager.Instance.NewBLinds(anteLevel);
    }

    // Phương thức để rút bài
    public CardSO DrawCard()
    {
        if (playingDeck.Count == 0)
        {
            return null;
        }

        if (playingDeck.Count > 0)
        {
            CardSO drawnCard = playingDeck[0];
            playingDeck.RemoveAt(0);
            return drawnCard;
        }

        return null; // Không còn bài để rút
    }

    public void PlayHand()
    {
        if (handCount <= 0 || choosingCards.Count == 0) return;
        totalScore += CalculateHandScore(handCards);
        handCount--;
        handCards.RemoveAll(card => choosingCards.Contains(card));
        playedCards.AddRange(choosingCards);

        UIManager.Instance.DestroyCard();
        choosingCards.Clear();

        NextRound();
    }
    public void CheckWinAnte()
    {
        if (totalScore < 300)
        {
            NextRound();
        } else
        {
            NextAnte();
        }
    }
    public void Discard()
    {
        if (discardsCount <= 0 || choosingCards.Count == 0) return;
        discardsCount--;
        handCards.RemoveAll(card => choosingCards.Contains(card));
        playingDeck.AddRange(choosingCards);
        UIManager.Instance.DestroyCard();
        choosingCards.Clear();
        Draws();
        UIManager.Instance.runUI.UpdateRun(this);

    }
    private void Shuffe()
    {
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

    public bool Choose(CardSO cardSO)
    {
        if (!choosingCards.Contains(cardSO))
        {
            if (choosingCards.Count >= 5) return false;
            choosingCards.Add(cardSO);
        } else
        {
            choosingCards.Remove(cardSO);
        }
        return true;
    }
    // Thêm phương thức tính điểm
    public int CalculateHandScore(List<CardSO> hand)
    {
        return 100; // Thay với logic tính điểm thật
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
    white = 1,
    red = 2,
    green = 3,
    black = 4,
    blue = 5,
    purple = 6,
    orange = 7,
    gold = 8
}