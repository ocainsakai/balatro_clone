using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class PokerManager : MonoBehaviour
{
    [SerializeField] GameContext gameContext;
    [SerializeField] PokerContext pokerContext;
    [SerializeField] CardManager cardManager;

    private void Awake()
    {
        gameContext.selectedCards.ObserveCountChanged().Subscribe(x =>
        {
            var cards = cardManager.GetCards(gameContext.selectedCards);
            PreviewPoker(cards);
        });
    }
    public void PreviewPoker(IEnumerable<Card> cards)
    {
        var result = PokerChecker.GetBestHand(cards);
        pokerContext.HandType.Value = result.HandType;
        pokerContext.IsScoredCard = cards.Where(x => result.MatchedCards.Contains(x.Data)).ToList();
    }
}
