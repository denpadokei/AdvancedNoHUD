using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;


namespace AdvancedNoHUD.UI.ViewControllers
{
    [HotReload(RelativePathToLayout = @"GameplayViewController.bsml")]
    [ViewDefinition("AdvancedNoHUD.UI.ViewControllers.GameplayViewController.bsml")]
    public class GameplayViewController : BSMLAutomaticViewController
    {
        [UIValue("setting-scene")]
        public string SettingScene { get; set; } = "Gameplay";

        private bool _hudEn = Configuration.PluginConfig.Instance.HMD.everything;
        [UIValue("enabled-bool")]
        public bool IngameHudEnabled
        {
            get => this._hudEn;
            set
            {
                this._hudEn = value;
                this.NotifyPropertyChanged();
                Configuration.PluginConfig.Instance.HMD.AllEnabled(value);
            }
        }

        private bool _comboEn = Configuration.PluginConfig.Instance.HMD.elements.combo;
        [UIValue("combo-bool")]
        public bool IngameComboEnabled
        {
            get => this._comboEn;
            set
            {
                this._comboEn = value;
                this.NotifyPropertyChanged();
            }
        }
        private bool _scoreEn = Configuration.PluginConfig.Instance.HMD.elements.score;
        [UIValue("score-bool")]
        public bool IngameScoreEnabled
        {
            get => this._scoreEn;
            set
            {
                this._scoreEn = value;
                this.NotifyPropertyChanged();
            }
        }
        private bool _rankEn = Configuration.PluginConfig.Instance.HMD.elements.rank;
        [UIValue("rank-bool")]
        public bool IngameRankEnabled
        {
            get => this._rankEn;
            set
            {
                this._rankEn = value;
                this.NotifyPropertyChanged();
            }
        }
        private bool _multiplierEn = Configuration.PluginConfig.Instance.HMD.elements.multiplier;
        [UIValue("multiplier-bool")]
        public bool IngameMultiplierEnabled
        {
            get => this._multiplierEn;
            set
            {
                this._multiplierEn = value;
                this.NotifyPropertyChanged();
            }
        }
        private bool _progressEn = Configuration.PluginConfig.Instance.HMD.elements.progress;
        [UIValue("progress-bool")]
        public bool IngameProgressEnabled
        {
            get => this._progressEn;
            set
            {
                this._progressEn = value;
                this.NotifyPropertyChanged();
            }
        }
        private bool _healthEn = Configuration.PluginConfig.Instance.HMD.elements.health;
        [UIValue("health-bool")]
        public bool IngameHealthEnabled
        {
            get => this._healthEn;
            set
            {
                this._healthEn = value;
                this.NotifyPropertyChanged();
            }
        }

        [UIAction("yes")]
        public void o()
        {
            var _AAAAAAA = new CustomTypes.HudElements(this.IngameComboEnabled, this.IngameScoreEnabled, this.IngameRankEnabled, this.IngameMultiplierEnabled, this.IngameProgressEnabled, this.IngameHealthEnabled);
            var _BBBBBBB = new CustomTypes.LocationPreset(CustomTypes.whereHUD.HMD, _AAAAAAA, this.IngameHudEnabled);
            Configuration.PluginConfig.Instance.HMD = _BBBBBBB;
        }

        public void Initialize()
        {
            Plugin.Log.Debug($"{this.GetType()}, Initialize call");
        }
    }
}
