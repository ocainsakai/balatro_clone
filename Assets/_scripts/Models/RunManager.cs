using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

[System.Serializable]
public class RunManager
{
    private AnteSO anteList => Resources.Load<AnteSO>(typeof(AnteSO).ToString());
    // Các thuộc tính cơ bản
    public StakeDifficult stakeDifficulty;
    public DeckSO deck;
    public Run run;
    public BlindSO currentBlind;

    // Thêm thuộc tính cho gameplay
    public List<CardSO> playingDeck = new List<CardSO>();
    public List<CardSO> choosingCards = new List<CardSO>();
    public List<CardSO> handCards = new List<CardSO>();
    public List<CardSO> playedCards = new List<CardSO>();
    public List<CardSO> jokers = new List<CardSO>();

    //public event Action<CardSO> onChoosing;
    // Thông tin về hands đã chơi
    public int handSize = 8;
    public int discardsCount = 4;
    public float scoreMultiplier = 1.0f;

    // Trạng thái game
    public bool isGameOver;
    public bool isBigWin;

    // Lưu kết quả các round
    public Dictionary<int, int> roundScores = new Dictionary<int, int>();

    // Constructor
    public RunManager(StakeDifficult stake, DeckSO selectedDeck, int startingMoney = 0)
    {
        stakeDifficulty = stake;
        deck = selectedDeck;
        run = new Run();
        InitializePlayingDeck();
    }

    private void InitializePlayingDeck()
    {
        playingDeck = new List<CardSO>(deck.defaultCards);
    }

    public int GetBaseChips()
    {
        Ante ante = anteList.antes[run.ante + 1];
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
    public void Draws()
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
        run.NextRound();
        Draws();
        UIManager.Instance.runUI.UpdateRun(run);
    }
    public void NextAnte()
    {
        run.NextAnte();
        BlindManager.Instance.NewBLinds(run.ante);
    }

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
        if (!run.PlayHand(CalculateHandScore(handCards)) || choosingCards.Count == 0) return;
        
        handCards.RemoveAll(card => choosingCards.Contains(card));
        playedCards.AddRange(choosingCards);

        UIManager.Instance.DestroyCard();
        choosingCards.Clear();

        CheckWinAnte();
    }
    public void CheckWinAnte()
    {
        if (run.isWinRound)
        {
            NextRound();
        } else
        {
            NextAnte();
        }
    }
    public void Discard()
    {
        if (run.Discard() || choosingCards.Count == 0) return;
        handCards.RemoveAll(card => choosingCards.Contains(card));
        playingDeck.AddRange(choosingCards);
        UIManager.Instance.DestroyCard();
        choosingCards.Clear();
        Draws();
        UIManager.Instance.runUI.UpdateRun(run);
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

    public void UpdateBlind(float multiple)
    {
        run.SetBlind(GetBaseChips(), multiple);
    }
    public void SaveRun()
    {
    }
}

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