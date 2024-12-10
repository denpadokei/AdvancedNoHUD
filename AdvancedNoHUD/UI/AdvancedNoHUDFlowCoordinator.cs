using AdvancedNoHUD.UI.ViewControllers;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using System;
using Zenject;

namespace AdvancedNoHUD.UI
{
    public class AdvancedNoHUDFlowCoordinator : FlowCoordinator, IInitializable
    {
        private GameplayViewController _gameplayViewController;
        private PauseViewController _pauseViewController;
        private LIVViewController _livViewController;

        [Inject]
        public void Constractor(GameplayViewController gameplayViewController, PauseViewController pauseViewController, LIVViewController livViewController)
        {
            this._gameplayViewController = gameplayViewController;
            this._pauseViewController = pauseViewController;
            this._livViewController = livViewController;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try {
                if (firstActivation) {
                    this.SetTitle("HUD Settings");
                    this.showBackButton = true;
                    this.ProvideInitialViewControllers(this._gameplayViewController, this._pauseViewController, this._livViewController);
                    //this.ProvideInitialViewControllers(this._gameplayViewController, this._pauseViewController);
                }
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
            this._gameplayViewController.Apply();
            this._pauseViewController.Apply();
            this._livViewController.Apply();
        }
        public void Initialize()
        {
            var menuButton = new MenuButton("AdvancedNoHUD", "Manage when and where the HUD is shown", this.ShowFlow);
            MenuButtons.Instance.RegisterButton(menuButton);
        }

        public void ShowFlow()
        {
            if (this._gameplayViewController == null || this._pauseViewController == null || this._livViewController == null) {
                return;
            }
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this);
        }
    }
}
