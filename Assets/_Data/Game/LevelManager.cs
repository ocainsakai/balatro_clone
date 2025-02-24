using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelSO> levels => Resources.Load<LevelListSO>(nameof(LevelListSO)).levels;

    public LevelSO GetLevel(int level)
    {
        return levels[level];
    }

}
