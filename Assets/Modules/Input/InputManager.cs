using Zenject;
using UniRx;

namespace Modules.Inputs
{
    public class InputManager : IInputManager
    {
        [Inject]
        private PlayerInputActions playerInputActions;

        public void OnShoot(int key)
        {
            this.Shoot.Execute(key);
        }

        public void OnPause()
        {
            this.Pause?.Invoke();
        }
        
        [Inject]
        public void Initialize()
        {
            this.playerInputActions.Player.Shoot0.performed += _=>OnShoot(0); 
            this.playerInputActions.Player.Shoot1.performed += _=>OnShoot(1);
            this.playerInputActions.Player.Shoot2.performed += _=>OnShoot(2);
            this.playerInputActions.Player.Shoot3.performed += _=>OnShoot(3);
            this.playerInputActions.Player.Shoot4.performed += _=>OnShoot(4);
            this.playerInputActions.Player.Shoot5.performed += _=>OnShoot(5);
            this.playerInputActions.Player.Shoot6.performed += _=>OnShoot(6);
            this.playerInputActions.Player.Shoot7.performed += _=>OnShoot(7);
            this.playerInputActions.Player.Shoot8.performed += _=>OnShoot(8);
            this.playerInputActions.Player.Shoot9.performed += _=>OnShoot(9);

            this.Shoot = new ReactiveCommand<int>();

            this.playerInputActions.Player.Pause.performed += _ => OnPause();
        }

        public IReactiveCommand<int> Shoot { get; private set; }
        public event IInputManager.PauseEventHandler Pause;
    }
}

