using Game.DataBase;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Overlay
{
    public class HelpInfoItem : ContextActionsItem<HelpInfo>
    {
        #region fields & properties
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image image;
        #endregion fields & properties

        #region methods
        protected override void UpdateUI()
        {
            base.UpdateUI();
            text.text = Context.Text.Text;
            image.sprite = Context.Sprite;
            image.enabled = Context.Sprite != null;
        }
        #endregion methods
    }
}