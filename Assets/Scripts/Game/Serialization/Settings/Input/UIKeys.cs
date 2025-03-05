using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Serialization.Settings.Input
{
    [System.Serializable]
    public class UIKeys : KeysCollection
    {
        #region fields & properties
        public KeyCodeInfo SettingsKey => settingsKey;
        [SerializeField] private KeyCodeInfo settingsKey = new(KeyCode.Escape, KeyCodeDescription.OpenSettings);
        public KeyCodeInfo InfoKey => infoKey;
        [SerializeField] private KeyCodeInfo infoKey = new(KeyCode.H, KeyCodeDescription.Info);
        #endregion fields & properties

        #region methods
        public override void ResetKeys()
        {
            settingsKey.Key = KeyCode.Escape;
            infoKey.Key = KeyCode.H;
        }
        public override List<KeyCodeInfo> GetKeys()
        {
            List<KeyCodeInfo> list = new()
            {
                SettingsKey,
                InfoKey,
            };
            return list;
        }
        #endregion methods
    }
}