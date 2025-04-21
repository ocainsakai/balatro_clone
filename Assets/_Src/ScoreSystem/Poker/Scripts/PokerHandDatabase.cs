using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PokerHandDatabase", menuName = "Scriptable Objects/PokerHandDatabase")]
public class PokerHandDatabase : ScriptableObject
{
    public List<PokerHandData> handDataList;

    private Dictionary<PokerHand, PokerHandData> handDataDict;

    public void Init()
    {
        handDataDict = new Dictionary<PokerHand, PokerHandData>();
        foreach (var data in handDataList)
        {
            handDataDict[data.handType] = data;
        }
    }

    public PokerHandData GetData(PokerHand handType)
    {
        if (handDataDict == null) Init();
        return handDataDict.TryGetValue(handType, out var data) ? data : null;
    }
}
