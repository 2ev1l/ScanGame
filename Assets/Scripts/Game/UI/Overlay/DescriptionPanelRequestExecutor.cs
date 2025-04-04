using Game.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class DescriptionPanelRequestExecutor : AdditionalPanelRequestExecutor
    {
        #region fields & properties
        [SerializeField] private TextMeshProUGUI text;
        #endregion fields & properties

        #region methods
        protected override bool CanExecuteAdditionalPanelRequest(AdditionalPanelRequest request)
        {
            if (request is not DescriptionPanelRequest) return false;
            return base.CanExecuteAdditionalPanelRequest(request);
        }
        protected override void UpdatePanelUI(AdditionalPanelRequest request)
        {
            base.UpdatePanelUI(request);
            text.text = ((DescriptionPanelRequest)request).Description;
        }
        #endregion methods
    }
}