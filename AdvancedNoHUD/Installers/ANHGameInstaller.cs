using Zenject;

namespace AdvancedNoHUD.Installers
{
    public class ANHGameInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<AdvancedNoHUDController>().FromNewComponentOnNewGameObject().AsCached().NonLazy();
        }
    }
}
