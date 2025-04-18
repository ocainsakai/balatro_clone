using UniRx;


namespace Game.Events
{
    public class GameEvents
    {
        public readonly Subject<Unit> OnGameStart = new Subject<Unit>();
        public readonly Subject<Unit> OnGamePause = new Subject<Unit>();
        public readonly Subject<int> OnScoreChanged = new Subject<int>();
    }
}

