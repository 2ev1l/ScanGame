using Game.DataBase;
using Game.Events;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Behaviour;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class AchievementObserver : Observer
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override void Dispose()
        {
            GameData.Data.AchievementsData.OnAchievementUnlocked -= OnAchievementSet;
        }
        public override void Initialize()
        {
            GameData.Data.AchievementsData.OnAchievementUnlocked += OnAchievementSet;
        }
        public void OnAchievementSet(int id)
        {
            new AchievementPanelRequest(DB.Instance.Achievements[id].Data).Send();
        }
        #endregion methods

    }
}