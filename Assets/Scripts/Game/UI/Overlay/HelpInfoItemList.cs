using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections.Generic;
using Universal.Core;

namespace Game.UI.Overlay
{
    public class HelpInfoItemList : ItemListBase<HelpInfoItem, HelpInfo>
    {
        #region fields & properties
        [SerializeField] private GameObject closeIcon;

        private readonly List<HelpInfo> helpInfos = new();
        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            base.OnEnable();
            ItemList.OnPageSwitched += OnPageSwitched;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            ItemList.OnPageSwitched -= OnPageSwitched;
        }
        private void OnPageSwitched()
        {
            if (closeIcon.activeSelf != ItemList.IsLastPage)
            {
                closeIcon.SetActive(ItemList.IsLastPage);
            }
        }
        public void GetInfo(List<HelpInfo> helpInfos)
        {
            helpInfos.SetElementsTo(this.helpInfos);
        }
        public override void UpdateListData()
        {
            ItemList.UpdateListDefault(helpInfos, x => x);
        }
        #endregion methods
    }
}