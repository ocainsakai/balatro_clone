using System;
using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    DeckManager deckManager => GetComponent<DeckManager>();
    BlindManager blindManager => GetComponent<BlindManager>();
    //PhaseManager phaseManager =  new PhaseManager();
    UIManager uiManager => GetComponent<UIManager>();
    public Phase currentPhase;
    public class Run {
        public int round_score;
        public int play_hands;
        public int discards;
        public int money;
        public int ante;
        public int round;
    }
    public Run run;
    public event Action sortByRank;
    public event Action sortBySuit;
    public event Action<Card> OnDrawCard;
    public event Action<Card> OnDiscardCard;
    public event Action<Run> OnRunUpdate;
    public event Action<Poker> OnPokerUpdate;
    public event Action<string> PhaseChanged;
    public bool canChoose => deckManager.choosing_cards.Count < 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        InitGame();
    }
    public void InitGame()
    {
        deckManager.Initialize();
        blindManager.Initialize();
        
        run = new Run()
        {
            round_score = 0,
            play_hands = 4,
            discards = 4,
            money = 0,
            ante = 1,
            round = 1
        };
        OnRunUpdate?.Invoke(run);
        OnPokerUpdate?.Invoke(new Poker());

        ChangePhase(Phase.blind);
    }

    public void ChangePhase(Phase newPhase)
    {
        currentPhase = newPhase;
        switch (currentPhase)
        {
            case Phase.blind:
                StartChoosePhase();
                break;
            case Phase.playing:
                StartPlayingPhase();
                break;
            case Phase.shop:
                StartShopPhase();
                break;
        }
    }

    public void StartChoosePhase()
    {
        uiManager.HideAll();
        uiManager.ShowBlind();
        PhaseChanged?.Invoke("Phase: Blind");
    }
    public void StartPlayingPhase()
    {
        uiManager?.HideAll();
        uiManager.ShowAction();
        uiManager.ShowHand();
        uiManager.ShowDeck();


        PhaseChanged?.Invoke("Phase: Play");
        StartCoroutine(DrawCoroutine());


    }
    public void StartShopPhase()
    {
        
        PhaseChanged?.Invoke("Phase: Shop");

    }
    public void StartGameOverPhase()
    {

    }
    public void PlayHand()
    {
        if (run.play_hands <= 0 || deckManager.choosing_cards.Count == 0) return;
        StartCoroutine(PlayHandCoroutine());
    }

    private IEnumerator PlayHandCoroutine()
    {
        int score = 0;
        yield return StartCoroutine(CalculateHand(result => score = result));

        run.round_score += score;
        run.play_hands--;
        run.round++;
        OnRunUpdate?.Invoke(run);

        if (CheckWin()) yield break;

        StartCoroutine(DiscardCoroutine());

        deckManager.PlayHand();
        StartCoroutine(DrawCoroutine());
    }


    public void Discard()
    {
        if (run.discards <= 0 || deckManager.choosing_cards.Count == 0) return;
        run.discards--;
        StartCoroutine(DiscardCoroutine());
        StartCoroutine(DrawCoroutine());
        deckManager.Shuffe();
        OnRunUpdate?.Invoke(run);
    }

    public void Choosing(Card card, bool isChoosing)
    {
        if (isChoosing)
        {
            deckManager.choosing_cards.Add(card);
        } else
        {
            deckManager.choosing_cards.Remove(card);
        }
        Poker poker;
        PokerHandEvaluator.EvaluateHand(deckManager.choosing_cards, out poker);
        OnPokerUpdate?.Invoke(poker);
        // poker hand and update ui

    }
    private IEnumerator DrawCoroutine()
    {
        for(int i = deckManager.hand_count; i < deckManager.hand_size; i++)
        {
            yield return StartCoroutine(deckManager.Draw(card => OnDrawCard?.Invoke(card)));
        }
    }
    private IEnumerator DiscardCoroutine()
    {
        foreach (var cardToDiscard in deckManager.choosing_cards)
        {
            
            yield return StartCoroutine(deckManager.Discard(cardToDiscard,card => OnDiscardCard?.Invoke(card)));
        }
    }
    public IEnumerator CalculateHand(Action<int> onComplete)
    {
        Poker poker = new Poker();
        PokerHandEvaluator.EvaluateHand(deckManager.choosing_cards, out poker);

        foreach (Card card in deckManager.choosing_cards)
        {
            poker.chips += card.baseValue;
            OnPokerUpdate?.Invoke(poker);
            yield return new WaitForSeconds(0.2f);
        }

        int score = (int)(poker.chips * poker.multiple);
        onComplete?.Invoke(score);
    }
    public bool CheckWin()
    {
        if (blindManager.IsWin(run.round_score))
        {
            Debug.Log("you defeat this blind");
            blindManager.Defeat();
            EndPlay();
            ChangePhase(Phase.blind);
            return true;
        } else if (run.play_hands <= 0)
        {
            //Debug.Log("you lose");
            ChangePhase(Phase.gameover);
            return true;
        }
        return false;

    }
    public void EndPlay()
    {
        OnRunUpdate?.Invoke(run);
        OnPokerUpdate?.Invoke(new Poker());

        deckManager.End();

    }
    public void SetBlind(Blind blind)
    {
        blindManager.SetCurrentBlind(blind);
        SetupRun();
    }

    public void SetupRun()
    {
        run.round_score = 0;
        run.play_hands = 4;
        run.discards = 4;
    }

    public void UpdateRunUI()
    {
        OnRunUpdate?.Invoke(run);
    }
    public void UpdatePokerUI(Poker poker)
    {
        OnPokerUpdate?.Invoke(poker);
    }
    public void SortByRank()
    {
        sortByRank?.Invoke();
    }
    public void SortBySuit()
    {
        sortBySuit?.Invoke();
    }
}


public enum Phase
{
    blind,
    playing,
    shop,
    gameover
}