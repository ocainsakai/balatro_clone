using System.Collections.Generic;
using UnityEngine;

public interface ICardCollection 
{
    List<CardModel> collection {  get; }
    List<CardModel> GetAllCards() => collection;

    CardModel DrawCard(int id);
    CardModel DrawCard();
    bool CanSelect();
    void AddCard(CardModel card);
    void AddCards(List<CardModel> cards);
    void SetCollection(List<CardData> collection);
    void RemoveCard(CardModel card);
    void RemoveCards(List<CardModel> cards);
    void RemoveAllCards();
    void Shuffe();
}
