using Game.Serialization.Settings;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Universal.Behaviour;
using Universal.Core;
using Universal.Serialization;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class ScreenSettings : IDisposable, Zenject.IInitializable
    {
        #region fields & properties
        #endregion fields & properties

        #region methods
        public void Initialize()
        {
            Awake();
            SceneLoader.OnSceneLoaded += Awake;
            SettingsData.Data.OnGraphicsChanged += OnGraphicsChanged;
        }
        public void Dispose()
        {
            SceneLoader.OnSceneLoaded -= Awake;
            SettingsData.Data.OnGraphicsChanged -= OnGraphicsChanged;
        }
        private void Awake()
        {
            OnGraphicsChanged(SettingsData.Data.GraphicsSettings);
        }
        private void OnGraphicsChanged(GraphicsSettings value)
        {
            Screen.SetResolution(value.Resolution.width, value.Resolution.height, value.ScreenMode);
            Application.targetFrameRate = value.RefreshRate;
            UnityEngine.QualitySettings.vSyncCount = value.Vsync ? 1 : 0;
        }
        #endregion methods
    }
}