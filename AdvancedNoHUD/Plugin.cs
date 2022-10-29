using AdvancedNoHUD.Installers;
using IPA;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace AdvancedNoHUD
{
    public enum WhereHUD
    {
        LIV = 1,
        HMD = 2,
        Pause = 3
    }

    public enum HUDelement
    {
        combo,
        score,
        rank,
        multiplier,
        progress,
        health
    }

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static Plugin Instance { get; private set; }
        public static IPALogger Log { get; private set; }

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger, IPA.Config.Config conf, Zenjector zenjector)
        {
            Instance = this;
            Log = logger;
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
            zenjector.Install<ANHMenuInstaller>(Location.Menu);
            zenjector.Install<ANHGameInstaller>(Location.Player);
            Log.Info("AdvancedNoHUD initialized.");
        }

        [OnStart]
        public void OnApplicationStart()
        {

        }

        [OnExit]
        public void OnApplicationQuit()
        {

        }
    }
}
