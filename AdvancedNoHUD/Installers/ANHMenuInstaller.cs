using AdvancedNoHUD.UI;
using AdvancedNoHUD.UI.ViewControllers;
using Zenject;

namespace AdvancedNoHUD.Installers
{
    public class ANHMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            _ = this.Container.BindInterfacesAndSelfTo<GameplayViewController>().FromNewComponentAsViewController().AsSingle();
            _ = this.Container.BindInterfacesAndSelfTo<LIVViewController>().FromNewComponentAsViewController().AsSingle();
            _ = this.Container.BindInterfacesAndSelfTo<PauseViewController>().FromNewComponentAsViewController().AsSingle();
            _ = this.Container.BindInterfacesAndSelfTo<AdvancedNoHUDFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
