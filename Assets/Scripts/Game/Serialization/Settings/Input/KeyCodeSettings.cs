using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Serialization.Settings.Input
{
    [System.Serializable]
    public class KeyCodeSettings : KeysCollection
    {
        #region fields & properties
        public KeyCodeInfo NoneKey => noneKey;
        [SerializeField] private KeyCodeInfo noneKey = new(KeyCode.None, KeyCodeDescription.None);
        public PlayerKeys PlayerKeys => playerKeys;
        [SerializeField] private PlayerKeys playerKeys = new();
        public UIKeys UIKeys => uiKeys;
        [SerializeField] private UIKeys uiKeys = new();
        #endregion fields & properties

        #region methods
        public override void ResetKeys()
        {
            noneKey.Key = KeyCode.None;
            playerKeys.ResetKeys();
            uiKeys.ResetKeys();
        }
        /// <summary>
        /// Provides original classes.
        /// </summary>
        /// <returns></returns>
        public override List<KeyCodeInfo> GetKeys()
        {
            List<KeyCodeInfo> list = new();

            list.AddRange(playerKeys.GetKeys());
            list.AddRange(uiKeys.GetKeys());
            return list;
        }
        #endregion methods
    }
}