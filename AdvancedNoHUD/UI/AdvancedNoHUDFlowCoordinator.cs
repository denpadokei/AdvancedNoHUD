using AdvancedNoHUD.UI.ViewControllers;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace AdvancedNoHUD.UI
{
    public class AdvancedNoHUDFlowCoordinator : FlowCoordinator, IInitializable
    {
        private GameplayViewController _GameplayViewController;
        private PauseViewController _PauseViewController;
        private LIVViewController _LIVViewController;

        [Inject]
        public void Constractor(GameplayViewController gameplayViewController, PauseViewController pauseViewController, LIVViewController livViewController)
        {
            this._GameplayViewController = gameplayViewController;
            this._PauseViewController = pauseViewController;
            this._LIVViewController = livViewController;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try {
                if (firstActivation) {
                    this.SetTitle("HUD Settings");
                    this.showBackButton = true;
                    //this.ProvideInitialViewControllers(this._GameplayViewController, this._PauseViewController, this._LIVViewController);
                    this.ProvideInitialViewControllers(this._GameplayViewController, this._PauseViewController);
                }
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
            this._GameplayViewController.o();
            this._PauseViewController.o();
            this._LIVViewController.o();
        }
        public void Initialize()
        {
            var menuButton = new MenuButton("AdvancedNoHUD", "Manage when and where the HUD is shown", this.ShowFlow);
            MenuButtons.instance.RegisterButton(menuButton);
        }

        public void ShowFlow()
        {
            if (this._GameplayViewController == null || this._PauseViewController == null || this._LIVViewController == null) {
                return;
            }
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this);
        }
    }
}
