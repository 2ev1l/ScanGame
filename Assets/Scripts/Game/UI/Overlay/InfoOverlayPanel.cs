using Game.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Universal.Events;

namespace Game.UI.Overlay
{
    public class InfoOverlayPanel : OverlayPanel<InfoRequest>
    {
        #region fields & properties
        public TextMeshProUGUI HeaderText => headerText;
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private HelpInfoItemList helpItemList;
        #endregion fields & properties

        #region methods
        protected override void UpdateUI(InfoRequest request)
        {
            HeaderText.text = request.HeaderInfo;
            helpItemList.GetInfo(request.HelpInfos);
            helpItemList.UpdateListData();
        }
        #endregion methods
    }
}