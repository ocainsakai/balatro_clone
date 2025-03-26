using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using BalatroClone.Cards;

public class Utilities 
{
    //public static bool isByRank;
    public static List<Card> SortByRank(List<Card> cards)
    {
        return cards.OrderBy(x => x.cardSO.rank)
                         .ThenBy(x => x.cardSO.suit)
                         .ToList();
    }
    public static List<CardSO> SortByRank(List<CardSO> cards)
    {
        return cards.OrderBy(x => x.rank)
                         .ThenBy(x => x.suit)
                         .ToList();
    }
    public static List<Card> SortBySuit(List<Card> cards)
    {
        return cards.OrderBy(x => x.cardSO.suit)
                         .ThenBy(x => x.cardSO.rank)
                         .ToList();
    }
    public static List<CardSO> SortBySuit(List<CardSO> cards)
    {
        return cards.OrderBy(x => x.suit)
                         .ThenBy(x => x.rank)
                         .ToList();
    }
    //public static void JumpText(TextMeshProUGUI text, string str, float duration)
    //{
    //    text.transform.localScale = Vector3.one;
    //    text.transform.DOScale(1.2f, duration * 0.5f)
    //        .SetEase(Ease.OutQuad)
    //    .OnComplete(() =>
    //    {
    //        text.text = str;
    //        text.transform.DOScale(1f, duration * 0.5f)
    //                .SetEase(Ease.InQuad);
    //    })
    //        .SetTarget(this);
    //}
}
