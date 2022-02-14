using UniRx;

namespace Modules.Inputs
{
    /// <summary>
    ///     Adapts inputs into game relevant events
    /// </summary>
    public interface IInputManager
    {
        /// <summary>
        ///     Triggered when the user shoots
        /// </summary>
        public IReactiveCommand<int> Shoot { get; }

        public delegate void PauseEventHandler();
        
        /// <summary>
        ///     Triggered when the user pauses the game.
        /// </summary>
        public event PauseEventHandler Pause;
    }
}