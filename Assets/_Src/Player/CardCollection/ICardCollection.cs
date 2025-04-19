using System.Collections.Generic;
using UnityEngine;

public interface ICardCollection 
{
    List<CardModel> collection {  get; }
    List<CardModel> GetAllCards() => collection;
    bool CanSelect();
    void AddCard(CardModel card);
    void RemoveCard(CardModel card);
    void RemoveAllCards();
}
