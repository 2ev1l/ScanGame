using Game.DataBase;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class AchievementItemList : ContextInfinityList<AchievementInfo>
{
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override void UpdateListData()
        {
            ItemList.UpdateListDefault(DB.Instance.Achievements.Data, x => x.Data);
        }
        #endregion methods
    }
}