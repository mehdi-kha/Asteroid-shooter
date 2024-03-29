﻿using UniRx;

namespace Modules.Game
{
    /// <summary>
    ///     Defines scoring and general game information.
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        ///     Current status of the game.
        /// </summary>
        IReactiveProperty<GameStatus> GameStatus { get; }
        
        /// <summary>
        ///     The score corresponds to the number of enemies killed.
        /// </summary>
        IReactiveProperty<int> Score { get; }
    }
}