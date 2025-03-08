using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class HelpInfos : DBInfo
    {
        #region fields & properties
        public IReadOnlyList<HelpInfo> Infos => infos;
        [SerializeField] private List<HelpInfo> infos = new();
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}