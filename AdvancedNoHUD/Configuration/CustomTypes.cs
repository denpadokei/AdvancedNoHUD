using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;

namespace AdvancedNoHUD.Configuration
{
    public class HudElements
    {
        public virtual bool Combo { get; set; } = true;
        public virtual bool Score { get; set; } = true;
        public virtual bool Rank { get; set; } = true;
        public virtual bool Multiplier { get; set; } = true;
        public virtual bool Progress { get; set; } = true;
        public virtual bool EnargyBar { get; set; } = true;

        public void SetAll(bool combo, bool score, bool rank, bool multiplier, bool progress, bool health)
        {
            this.Combo = combo;
            this.Score = score;
            this.Rank = rank;
            this.Multiplier = multiplier;
            this.Progress = progress;
            this.EnargyBar = health;
        }

        public HudElements()
        {

        }

        public HudElements(bool combo, bool score, bool rank, bool multiplier, bool progress, bool health)
        {
            this.Combo = combo;
            this.Score = score;
            this.Rank = rank;
            this.Multiplier = multiplier;
            this.Progress = progress;
            this.EnargyBar = health;
        }
    }

    public class LocationPreset
    {
        [UseConverter(typeof(EnumConverter<WhereHUD>))]
        public virtual WhereHUD Where { get; set; } = WhereHUD.HMD;
        public virtual bool Everything { get; set; } = false;
        [NonNullable]
        public virtual HudElements Elements { get; set; } = new HudElements();

        public void AllEnabled(bool yes)
        {
            this.Everything = yes;
        }
        public void SetWhere(WhereHUD w)
        {
            this.Where = w;
        }

        public LocationPreset()
        {
        }

        public LocationPreset(WhereHUD where, HudElements elements, bool everything = true)
        {
            this.Where = where;
            this.Elements = elements;
            this.Everything = everything;
        }
    }
}
