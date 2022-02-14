using UniRx;

namespace Modules.Inputs
{
    /// <summary>
    ///     Adapts inputs into game relevant events
    /// </summary>
    public interface IInputManager
    {
        public IReactiveCommand<int> Shoot { get; }
        
        public delegate void PauseEventHandler();
        public event PauseEventHandler Pause;
    }
}