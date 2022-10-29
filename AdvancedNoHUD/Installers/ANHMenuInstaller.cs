using AdvancedNoHUD.UI;
using AdvancedNoHUD.UI.ViewControllers;
using Zenject;

namespace AdvancedNoHUD.Installers
{
    public class ANHMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<GameplayViewController>().FromNewComponentAsViewController().AsSingle();
            this.Container.BindInterfacesAndSelfTo<LIVViewController>().FromNewComponentAsViewController().AsSingle();
            this.Container.BindInterfacesAndSelfTo<PauseViewController>().FromNewComponentAsViewController().AsSingle();
            this.Container.BindInterfacesAndSelfTo<AdvancedNoHUDFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
