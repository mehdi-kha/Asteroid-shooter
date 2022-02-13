using UniRx;

namespace Modules.Game
{
    public interface IGameModel
    {
        IReactiveProperty<GameStatus> GameStatus { get; }
    }
}