﻿
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace AdvancedNoHUD.Configuration
{
    public class PluginConfig
    {
        public static PluginConfig Instance { get; set; }
        [NonNullable]
        public virtual LocationPreset HMD { get; set; } = new LocationPreset(WhereHUD.HMD, new HudElements(), false);
        [NonNullable]
        public virtual LocationPreset LIV { get; set; } = new LocationPreset(WhereHUD.LIV, new HudElements(), false);
        [NonNullable]
        public virtual LocationPreset Pause { get; set; } = new LocationPreset(WhereHUD.Pause, new HudElements(), false);

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}
