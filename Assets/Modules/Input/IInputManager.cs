using UniRx;

namespace Modules.Inputs
{
    public interface IInputManager
    {
        public IReactiveCommand<int> Shoot { get; }
    }
}