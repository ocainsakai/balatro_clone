using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Bet Database")]

public class BetDatabase : ScriptableObject
{
    public List<BetOption> options;

}
