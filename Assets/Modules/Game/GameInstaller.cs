using Zenject;

namespace Modules.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();
        }
    }
}