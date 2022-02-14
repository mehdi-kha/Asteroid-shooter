using Zenject;

namespace Modules.Enemies
{
    public class EnemiesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<EnemiesModel>().AsSingle();
        }
    }
}