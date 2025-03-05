using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Serialization.Settings.Input
{
    [System.Serializable]
    public class PlayerKeys : KeysCollection
    {
        #region fields & properties
        public KeyCodeInfo InteractKey => interactKey;
        [SerializeField] private KeyCodeInfo interactKey = new(KeyCode.E, KeyCodeDescription.Interact);
        #endregion fields & properties

        #region methods
        public override void ResetKeys()
        {
            interactKey.Key = KeyCode.E;
        }
        public override List<KeyCodeInfo> GetKeys()
        {
            List<KeyCodeInfo> list = new()
            {
                InteractKey,
            };
            return list;
        }
        #endregion methods
    }
}