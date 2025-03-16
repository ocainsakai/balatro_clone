
public class Run
{
    public int round;
    public int ante;
    public int money;
    public int handCount;
    public int discardCount;
    public int blindScore;
    public int roundScore;

    public bool isWinRound => roundScore >= blindScore;

    public Run(){
        round = 1;
        ante = 1;
        money = 0;
        handCount = 4;
        discardCount = 4;
        blindScore = 0;
        roundScore = 0;
    }

    public void NextRound()
    {
        round++;
    }
    public void NextAnte()
    {
        round++;
        ante++;
    }
    public bool PlayHand(int roundScore)
    {
        if(handCount <=0) return false;
        handCount--;
        this.roundScore += roundScore;
        return true;
    }
    public bool Discard()
    {
        if(discardCount <=0) return false;
        discardCount--;
        return true;
    }
    public void SetBlind(int baseChips, float multiple)
    {
        blindScore = (int)(baseChips * multiple);
    }
}
