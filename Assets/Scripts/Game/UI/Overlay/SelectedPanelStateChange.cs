using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Overlay
{
    public class SelectedPanelStateChange : PanelStateChange
    {
        #region fields & properties
        [SerializeField] private Image raycast;
        [SerializeField] private bool hasSelectableInfo = true;
        [SerializeField][DrawIf(nameof(hasSelectableInfo), true)] private GameObject selectInfo;
        #endregion fields & properties

        #region methods
        public override void SetActive(bool active)
        {
            base.SetActive(active);
            raycast.enabled = !active;
            if (hasSelectableInfo && selectInfo.activeSelf != active)
                selectInfo.SetActive(active);
        }
        #endregion methods
    }
}