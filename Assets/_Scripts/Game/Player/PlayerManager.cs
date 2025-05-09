using UniRx;

namespace Game.Player
{
    public class PlayerManager
    {
        public ReactiveProperty<int> HandCount = new();
        public ReactiveProperty<int> Discards = new();
        public ReactiveProperty<int> Money = new();
    }
}