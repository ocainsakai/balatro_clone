using System.Collections.Generic;

public static class ScoreCalculator
{
    public static int CalculateScore(PokerHandType handType)
    {
        return ScoreMap.BaseScores[handType] * ScoreMap.Multipliers[handType];
    }
}
