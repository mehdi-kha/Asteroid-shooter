using Modules.Game;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        this.Container.BindInterfacesAndSelfTo<GameModel>().AsSingle();
    }
}