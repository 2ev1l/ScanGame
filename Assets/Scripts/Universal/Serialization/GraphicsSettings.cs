using UnityEngine;
using UnityEngine.Events;

namespace Universal.Serialization
{
    [System.Serializable]
    public class GraphicsSettings
    {
        #region fields & properties
        public SimpleResolution Resolution
        {
            get
            {
                if (resolution.width == 0 || resolution.height == 0)
                {
                    resolution.width = Screen.currentResolution.width;
                    resolution.height = Screen.currentResolution.height;
                }
                
                return resolution;
            }
            set => SetResolution(value);
        }
        [SerializeField] private SimpleResolution resolution;
        public FullScreenMode ScreenMode
        {
            get => screenMode;
            set => SetScreenMode(value);
        }
        [SerializeField] private FullScreenMode screenMode = FullScreenMode.FullScreenWindow;
        public bool Vsync
        {
            get => vsync;
            set => SetVsync(value);
        }
        [SerializeField] private bool vsync = false;
        /// <summary>
        /// 10..inf
        /// </summary>
        public int RefreshRate
        {
            get => refreshRate;
            set => SetRefreshRate(value);
        }
        [SerializeField] private int refreshRate = 60;

        #endregion fields & properties

        #region methods
        private void SetResolution(SimpleResolution value)
        {
            resolution = value;
        }
        private void SetScreenMode(FullScreenMode value)
        {
            screenMode = value;
        }
        private void SetVsync(bool value)
        {
            vsync = value;
        }
        private void SetRefreshRate(int value)
        {
            value = Mathf.Max(value, 10);
            refreshRate = value;
        }
        public GraphicsSettings() { }
        public GraphicsSettings(SimpleResolution resolution, FullScreenMode screenMode, bool vsync, int refreshRate)
        {
            Resolution = resolution;
            ScreenMode = screenMode;
            Vsync = vsync;
            RefreshRate = refreshRate;
        }
        #endregion methods
    }
}