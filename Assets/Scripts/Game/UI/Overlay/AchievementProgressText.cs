using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class AchievementProgressText : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private TextMeshProUGUI text;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            UpdateUI();
        }
        private void UpdateUI()
        {
            int totalAchievements = DB.Instance.Achievements.Data.Count;
            int completedAchievements = GameData.Data.AchievementsData.UnlockedAchievements.Count;
            text.text = $"{completedAchievements} / {totalAchievements}";
        }
        #endregion methods
    }
}