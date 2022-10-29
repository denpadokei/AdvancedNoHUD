using AdvancedNoHUD.Configuration;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace AdvancedNoHUD.UI.ViewControllers
{
    [HotReload(RelativePathToLayout = @"LIVViewController.bsml")]
    [ViewDefinition("AdvancedNoHUD.UI.ViewControllers.LIVViewController.bsml")]
    public class LIVViewController : BSMLAutomaticViewController
    {
        public void Initialize()
        {
            Plugin.Log.Debug($"{this.GetType()}, Initialize call");
        }

        [UIValue("setting-scene")]
        public string SettingScene { get; set; } = "LIV";

        private bool _hudEn = PluginConfig.Instance.LIV.Everything;
        [UIValue("enabled-bool")]
        public bool IngameHudEnabled
        {
            get => this._hudEn;
            set
            {
                this._hudEn = value;
                this.NotifyPropertyChanged();
            }
        }

        private bool _comboEn = PluginConfig.Instance.LIV.Elements.Combo;
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
        private bool _scoreEn = PluginConfig.Instance.LIV.Elements.Score;
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
        private bool _rankEn = PluginConfig.Instance.LIV.Elements.Rank;
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
        private bool _multiplierEn = PluginConfig.Instance.LIV.Elements.Multiplier;
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
        private bool _progressEn = PluginConfig.Instance.LIV.Elements.Progress;
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
        private bool _healthEn = PluginConfig.Instance.LIV.Elements.EnargyBar;
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
        public void Apply()
        {
            var element = new HudElements(this.IngameComboEnabled, this.IngameScoreEnabled, this.IngameRankEnabled, this.IngameMultiplierEnabled, this.IngameProgressEnabled, this.IngameHealthEnabled);
            var preset = new LocationPreset(WhereHUD.LIV, element, this.IngameHudEnabled);
            preset.AllEnabled(this.IngameHudEnabled);
            PluginConfig.Instance.LIV = preset;
        }
    }
}
