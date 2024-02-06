using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;

namespace AdvancedNoHUD.Configuration
{
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
