using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckSO", menuName = "Scriptable Objects/DeckSO")]
public class DeckSO : CollectableSO
{
    public Sprite cardBackImage;

    public List<CardSO> defaultCards = new List<CardSO>();

    [Tooltip("Điều kiện để mở khóa deck này")]
    public string unlockCondition;

}
