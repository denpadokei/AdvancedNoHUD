using AdvancedNoHUD.CustomTypes;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace AdvancedNoHUD
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class AdvancedNoHUDController : MonoBehaviour
    {
        private GameObject _combo;
        private GameObject _score;
        private GameObject _rank;
        private GameObject _multiplier;
        private GameObject _progress;
        private GameObject _health;
        private IGamePause _gamePause;
        private IAudioTimeSource _audioTimeSource;
        private float _startTime = 0f;
        private WaitWhile _waitWhile;
        private static readonly int s_hiddenHudLayer = 23;
        private static readonly int s_normalHudLayer = 5;
        private bool _found = false;

        //private readonly Camera LIVCam;
        private GameObject _hud;

        [Inject]
        public void Constractor(IGamePause gamePause, IAudioTimeSource audio)
        {
            this._gamePause = gamePause;
            this._audioTimeSource = audio;
            this._startTime = audio.songTime;
        }

        public void Awake()
        {
            this._waitWhile = new WaitWhile(() => this._audioTimeSource.songTime <= this._startTime);
            this._gamePause.didPauseEvent += this.GamePause_didPauseEvent;
            this._gamePause.didResumeEvent += this.GamePause_didResumeEvent;
        }

        public IEnumerator Start()
        {
            yield return this._waitWhile;
#if DEBUG
            Plugin.Log.Info("AudioTimeSyncController.StartSong()");
#endif
            if (this.FindHUD()) {
                this._found = true;
                this.FindHUDElements();
                this.PutThings(CustomTypes.whereHUD.HMD);
            }
#if DEBUG
            Plugin.Log.Info("Finished Setup");
#endif
        }

        public void OnDestroy()
        {
            this._gamePause.didPauseEvent -= this.GamePause_didPauseEvent;
            this._gamePause.didResumeEvent -= this.GamePause_didResumeEvent;
        }

        private void GamePause_didPauseEvent()
        {
#if DEBUG
            Plugin.Log.Info("AudioTimeSyncController.Pause()");
#endif
            if (this._found) {
                this.PutThings(CustomTypes.whereHUD.Pause);
            }
        }
        private void GamePause_didResumeEvent()
        {
#if DEBUG
            Plugin.Log.Info("AudioTimeSyncController.Resume()");
#endif
            if (this._found) {
                this.PutThings(CustomTypes.whereHUD.HMD);
            }
        }

        public bool AssignObject(ref GameObject assign, string name)
        {
            try {
                assign = GameObject.Find(name);
                Plugin.Log.Debug($"Found {name}");
                return true;
            }
            catch (Exception) {
                Plugin.Log.Critical($"{name} could not be found!)");
                return false;
            }
        }

        public void VerboseActive(ref GameObject objec, bool status, string name = "")
        {
            try {
                objec.SetActive(status);
            }
            catch (NullReferenceException) {

                Plugin.Log.Critical($"NullReferenceException when setting status of {name}");
            }
            catch (Exception e) {
                Plugin.Log.Error(e);
            }
        }

        public void FindHUDElements()
        {
            if (this._hud == null) {
                this.FindHUD();
            }
            //i know this is jank but in case some idiot has counters+ installs and breaks shit
            this.AssignObject(ref this._combo, "ComboPanel");
            this.AssignObject(ref this._score, "ScoreText");
            if (this.AssignObject(ref this._rank, "ImmediateRankText")) {
                GameObject.Find("RelativeScoreText").gameObject.transform.SetParent(this._rank.transform);
            }

            this.AssignObject(ref this._multiplier, "MultiplierCanvas");
            this.AssignObject(ref this._progress, "SongProgressCanvas");
            this.AssignObject(ref this._health, "EnergyPanel");
        }

        public void PutThings(whereHUD wh)
        {
            var tempPreset = new LocationPreset();
            switch (wh) {
                case whereHUD.HMD:
                    tempPreset = Configuration.PluginConfig.Instance.HMD;
                    break;
                case whereHUD.Pause:
                    tempPreset = Configuration.PluginConfig.Instance.Pause;
                    break;
            }
            try {
                this.VerboseActive(ref this._combo, tempPreset.elements.combo, "Combo");
                this.VerboseActive(ref this._score, tempPreset.elements.score, "Score");
                this.VerboseActive(ref this._rank, tempPreset.elements.rank, "Rank");
                this.VerboseActive(ref this._multiplier, tempPreset.elements.multiplier, "Multiplier");
                this.VerboseActive(ref this._progress, tempPreset.elements.progress, "Progress");
                this.VerboseActive(ref this._health, tempPreset.elements.health, "Health");
            }
            catch (NullReferenceException) {
                this.FindHUDElements();
                this.PutThings(wh);
            }
        }
        public void HideInLiv()
        {
            var elements = Configuration.PluginConfig.Instance.LIV.elements;

            this._combo.layer = !elements.combo ? 23 : 5;

            this._score.layer = !elements.score ? 23 : 5;

            this._rank.layer = !elements.rank ? 23 : 5;

            this._multiplier.layer = !elements.multiplier ? 23 : 5;

            this._progress.layer = !elements.progress ? 23 : 5;

            this._health.layer = !elements.health ? 23 : 5;

        }
        public void FindLivCamera()
        {
            int hudToggle(int flag, bool show = true)
            {
                return show ? flag | (1 << s_hiddenHudLayer) : flag & ~(1 << s_hiddenHudLayer);
            }

            foreach (var cam in Resources.FindObjectsOfTypeAll<Camera>()) {
                if (cam.name == "MainCamera") {
                    cam.cullingMask = hudToggle(cam.cullingMask, false);

                    if (cam.name != "MainCamera") {
                        continue;
                    }

                    var x = cam.GetComponent<LIV.SDK.Unity.LIV>();

                    if (x != null) {
                        x.spectatorLayerMask = s_hiddenHudLayer;
                    }
                }
                else {
                    cam.cullingMask = hudToggle(cam.cullingMask, (cam.cullingMask & (1 << s_normalHudLayer)) != 0);
                }
            }
        }

        /// <summary>
        /// Finds the HUD
        /// </summary>
        /// <returns>true if found, false if not</returns>
        public bool FindHUD()
        {
            this._hud = GameObject.Find("BasicGameHUD");
            if (this._hud == null) {
                this._hud = GameObject.Find("NarrowGameHUD");
            }

            if (this._hud == null) {
                this._hud = GameObject.Find("FlyingGameHUD");
            }

            if (this._hud == null) {
                Plugin.Log.Notice("HUD was not found!");
            }

            Plugin.Log.Info($"HUD name is: \"{this._hud.name}\"");
            return this._hud != null;
        }
    }
}
