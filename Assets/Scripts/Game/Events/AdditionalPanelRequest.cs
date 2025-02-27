using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Events;

namespace Game.Events
{
    [System.Serializable]
    public class AdditionalPanelRequest : ExecutableRequest
    {
        #region fields & properties
        public bool Open => open;
        private bool open = false;
        #endregion fields & properties

        #region methods
        public AdditionalPanelRequest(bool open)
        {
            this.open = open;
        }
        public override void Close() { }
        #endregion methods
    }
}