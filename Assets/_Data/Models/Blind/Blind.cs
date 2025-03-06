
public class Blind
{
    public BlindSO blindSO;
    public int score_at_least;
    public void SetScore(int base_score)
    {
        this.score_at_least = (int)(base_score * blindSO.score_multiple);
    }
}
