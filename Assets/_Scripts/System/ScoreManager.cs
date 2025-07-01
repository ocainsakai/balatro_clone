using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] ScoreContext scoreContext;
    [SerializeField] GameContext gameContext;
    [SerializeField] PokerContext pokerContext;
    [SerializeField] EffectManager effectManager;
    [SerializeField] JokerManager jokerManager;
    GridObjectLayout2D grid;

    private void Awake()
    {
        grid = GetComponentInChildren<GridObjectLayout2D>();
    }
    public void AddSelect()
    {
        _ = ScoringCard(pokerContext.IsScoredCard);
    }
    public async UniTask ScoringCard(IEnumerable<Card> cards)
    {
        await OnScore(cards);
        await PostScore();
        CalculateScore();
    }
    private void CalculateScore()
    {
        int chip = pokerContext.Chip.Value;
        int mult = pokerContext.Mult.Value;
        scoreContext.RoundScore.Value += (chip * mult);

        
    }
    private async UniTask OnScore(IEnumerable<Card> cards)
    {
        for (int i = 0; i < cards.Count(); i++)
        {
            var card = cards.ElementAt(i);
            await card.AddChip();
            await OnScoreJoker(card);
        }
    }

    public async UniTask OnScoreJoker(Card data)
    {
        foreach (var joker in jokerManager.jokers)
        {
            await joker.OnScoredActive(data);
        }
    }
    public async UniTask PostScore()
    {
        foreach (var joker in jokerManager.jokers)
        {
            await joker.PostActive();
        }

    }
}
