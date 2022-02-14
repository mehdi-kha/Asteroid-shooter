using Zenject;

namespace Modules.Inputs
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
            Container.BindInstance(playerInputActions);
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }
    }
}