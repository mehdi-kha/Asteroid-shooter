using UnityEngine;
using Zenject;

namespace Modules.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIModel uiModel;
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<UIModel>().FromInstance(uiModel);
        }
    }
}