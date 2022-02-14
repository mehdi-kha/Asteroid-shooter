using UniRx;

namespace Modules.Game
{
    public class GameModel : IGameModel
    {
        public IReactiveProperty<GameStatus> GameStatus { get; private set; }
        public IReactiveProperty<int> Score { get; private set; }

        public GameModel()
        {
            this.GameStatus = new ReactiveProperty<GameStatus>(Game.GameStatus.NotStarted);
            this.Score = new ReactiveProperty<int>(0);
        }
    }
}