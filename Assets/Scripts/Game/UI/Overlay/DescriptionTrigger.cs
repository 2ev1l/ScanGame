using EditorCustom.Attributes;
using Game.DataBase;
using Game.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class DescriptionTrigger : MonoBehaviour
    {
        #region fields & properties
        public LanguageInfo DescriptionInfo
        {
            get => descriptionInfo;
            set => descriptionInfo = value;
        }
        [SerializeField] private LanguageInfo descriptionInfo = new(0, TextType.Game);
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void ShowDescriptionPanel()
        {
            new DescriptionPanelRequest(true, DescriptionInfo).Send();
        }
        [SerializedMethod]
        public void HideDescriptionPanel()
        {
            new DescriptionPanelRequest(false, DescriptionInfo).Send();
        }
        #endregion methods
    }
}