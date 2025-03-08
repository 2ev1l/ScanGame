using EditorCustom.Attributes;
using Game.DataBase;
using Game.Events;
using Game.Serialization.World;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Universal.Core;

namespace Game.UI.Overlay
{
    public class AchievementItem : ContextActionsItem<AchievementInfo>
    {
        #region fields & properties
        [SerializeField] private Image preview;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Color lockedColor;
        [SerializeField] private GameObject lockedInfo;
        [SerializeField] private DescriptionTrigger descriptionTrigger;
        #endregion fields & properties

        #region methods
        protected override void UpdateUI()
        {
            base.UpdateUI();
            nameText.text = Context.NameInfo.Text;
            int id = Context.Id;
            bool isAchievementUnlocked = GameData.Data.AchievementsData.UnlockedAchievements.Exists(x => x == id, out _);
            preview.sprite = Context.PreviewSprite;
            preview.color = isAchievementUnlocked ? Color.white : lockedColor;
            lockedInfo.SetActive(!isAchievementUnlocked);
            descriptionTrigger.DescriptionInfo = Context.DescriptionInfo;
        }
        #endregion methods
    }
}