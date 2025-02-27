using EditorCustom.Attributes;
using Game.DataBase;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class MiniGameItemList : ContextInfinityList<MiniGameInfo>
    {
        #region fields & properties
        [Title("Mini Games")]
        [SerializeField] private MiniGameStageSO miniGameStage;
        [SerializeField] private TextMeshProUGUI header;
        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            base.OnEnable();
            header.text = miniGameStage.Data.NameInfo.Text;
        }
        public override void UpdateListData()
        {
            ItemList.UpdateListDefault(miniGameStage.Data.MiniGames, x => x.Data);
        }
        #endregion methods
    }
}