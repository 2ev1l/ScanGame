using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Events;

namespace Game.Events
{
    [System.Serializable]
    public class AchievementPanelRequest : ExecutableRequest
    {
        #region fields & properties
        public AchievementInfo Info => info;
        [SerializeField] private AchievementInfo info;
        #endregion fields & properties

        #region methods
        public override void Close() { }
        public AchievementPanelRequest(AchievementInfo info)
        {
            this.info = info;
        }
        #endregion methods
    }
}