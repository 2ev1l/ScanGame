using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class MiniGameState : PanelStateChange
    {
        #region fields & properties
        public MiniGameInfo GameInfo => gameInfo.Data;
        [SerializeField] private MiniGameInfoSO gameInfo;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}