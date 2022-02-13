using Modules.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIModel uiModel;
    public override void InstallBindings()
    {
        this.Container.BindInterfacesAndSelfTo<UIModel>().FromInstance(uiModel);
    }
}