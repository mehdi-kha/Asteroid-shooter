using Modules.Enemies;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        this.Container.BindInterfacesAndSelfTo<EnemiesModel>().AsSingle();
    }
}