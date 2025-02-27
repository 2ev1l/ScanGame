using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Universal.Collections.Generic;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class AchievementsData
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> - Achievement Id
        /// </summary>
        public UnityAction<int> OnAchievementUnlocked;
        public IReadOnlyList<int> UnlockedAchievements => unlockedAchievements.Items;
        [SerializeField] private UniqueList<int> unlockedAchievements = new();
        #endregion fields & properties

        #region methods
        public bool TryUnlockAchievement(int id)
        {
            if (!unlockedAchievements.TryAddItem(id, x => x == id))
                return false;
            OnAchievementUnlocked?.Invoke(id);
            return true;
        }
        #endregion methods
    }
}