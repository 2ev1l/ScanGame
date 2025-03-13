using EditorCustom.Attributes;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class AchievementTrigger : MonoBehaviour
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void SetAchievement(int id)
        {
            GameData.Data.AchievementsData.TryUnlockAchievement(id);
        }
        #endregion methods
    }
}