
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : SingletonAbs<DeckManager>
{
    [SerializeField] UIHand uiDeck;
    
    [SerializeField] public List<CardSO> defaulsCards 
        => Resources.LoadAll<CardSO>("CardSO").ToList();
   
    public List<CardSO> playingCards;

    public void Start()
    {
        playingCards = new List<CardSO>(defaulsCards);
        //evaluator = new PokerHandEvaluator();
        //Debug.Log(playingCards.Count);
        Initlize();
    }
    public void Initlize()
    {
        uiDeck.Initlize(defaulsCards);

    }
}
