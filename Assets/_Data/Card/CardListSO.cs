using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardListSO", menuName = "Scriptable Objects/CardListSO")]
public class CardListSO : ScriptableObject
{
    public List<CardSO> cards;
}
