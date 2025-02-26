using Universal.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Game.UI.Collections;
using Game.UI.Elements;
using UnityEngine.Rendering.Universal;

namespace Game.UI.Settings
{
    public class GraphicsPanel : SettingsPanel<GraphicsSettings>
    {
        #region fields & properties
        protected override GraphicsSettings Settings
        {
            get => Context.GraphicsSettings;
            set => Context.GraphicsSettings = value;
        }
        [SerializeField] private ResolutionsItemList resolutionsList;
        [SerializeField] private Slider fpsSlider;
        [SerializeField] private CustomCheckbox vsyncCheckbox;
        [SerializeField] private IntItemList screenModeList;
        [SerializeField] private IntItemList qualityPresetList;
        [SerializeField] private List<UniversalRenderPipelineAsset> URPAssets;

        private List<SimpleResolution> Resolutions
        {
            get
            {
                resolutions ??= GetResolutions();
                return resolutions;
            }
        }
        private List<SimpleResolution> resolutions = null;
        private List<int> ScreenModes
        {
            get
            {
                screenModes ??= new() { (int)FullScreenMode.ExclusiveFullScreen, (int)FullScreenMode.FullScreenWindow, (int)FullScreenMode.Windowed };
                return screenModes;
            }
        }
        private List<int> screenModes = null;
        private List<int> QualityPresets
        {
            get
            {
                if (qualityPresets == null)
                {
                    int urpAssetsCount = URPAssets.Count;
                    qualityPresets = new();
                    for (int i = 0; i < urpAssetsCount; ++i)
                        qualityPresets.Add(i);
                }
                return qualityPresets;
            }
        }
        private List<int> qualityPresets = null;

        private int MaxFPS
        {
            get
            {
                if (maxFPS == -1)
                    maxFPS = GetMaxFPS();
                return maxFPS;
            }
        }
        private int maxFPS = -1;
        #endregion fields & properties

        #region methods
        public override GraphicsSettings GetNewSettings()
        {
            SimpleResolution resolution = ExposeItemList(resolutionsList);
            FullScreenMode screenMode = (FullScreenMode)ExposeItemList(screenModeList);
            int fps = (int)ExposeSlider(fpsSlider);
            bool vsync = vsyncCheckbox.CurrentState;
            GraphicsSettings newSettings = new(resolution, screenMode, vsync, fps);
            return newSettings;
        }
        public override void SetNewSettings()
        {
            base.SetNewSettings();
            UnityEngine.QualitySettings.SetQualityLevel(ExposeItemList(qualityPresetList));
        }
        public override void UpdateUI()
        {
            fpsSlider.value = Settings.RefreshRate;
            fpsSlider.maxValue = MaxFPS;
            vsyncCheckbox.CurrentState = Settings.Vsync;

            screenModeList.SetItems(ScreenModes);
            screenModeList.ItemList.ShowAt((int)Settings.ScreenMode);
            resolutionsList.SetItems(Resolutions);
            resolutionsList.ItemList.ShowAt(Settings.Resolution);

            int currentURP = UnityEngine.QualitySettings.GetQualityLevel();
            qualityPresetList.SetItems(QualityPresets);
            qualityPresetList.ItemList.ShowAt(currentURP);
        }
        private List<SimpleResolution> GetResolutions()
        {
            List<SimpleResolution> simpleResolutions = new();
            var resolutions = Screen.resolutions;
            foreach (Resolution res in resolutions)
            {
                if (simpleResolutions.Exists(x => x.width == res.width && x.height == res.height)) continue;
                SimpleResolution sRes = new()
                {
                    width = res.width,
                    height = res.height
                };
                simpleResolutions.Add(sRes);
            }
            simpleResolutions = simpleResolutions.OrderBy(x => x.width).ToList();
            return simpleResolutions;
        }
        private int GetMaxFPS() => (int)Screen.resolutions.Max(x => x.refreshRateRatio).value + 1;
        #endregion methods
    }
}