using Balatro.Card;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CardManager cardManager;
    [SerializeField] List<PlayingCardSO> playingCards;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardManager.SetPlayingDeck(playingCards);
        cardManager.NewRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
