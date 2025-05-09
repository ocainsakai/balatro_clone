
using VContainer;
using Game.Cards;
namespace Game.System.Score
{
    public class ScoreManagerView : GridViewBase<CardView>
    {
        private ScoreManager scoreManager;
        //AsyncProcess scoreProcess = new AsyncProcess();

        [Inject] 
        public void Construct(ScoreManager scoreManager)
        {
            this._cards = scoreManager;
            this.scoreManager = scoreManager;
        }
    }
}