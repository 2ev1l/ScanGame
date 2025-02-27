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
        }
        [SerializedMethod]
        public void ShowDescriptionPanel()
        {
            new DescriptionPanelRequest(true, Context.DescriptionInfo).Send();
        }
        [SerializedMethod]
        public void HideDescriptionPanel()
        {
            new DescriptionPanelRequest(false, Context.DescriptionInfo).Send();
        }
        #endregion methods
    }
}