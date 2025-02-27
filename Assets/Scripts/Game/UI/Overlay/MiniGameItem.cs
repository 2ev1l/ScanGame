using EditorCustom.Attributes;
using Game.DataBase;
using Game.Serialization.World;
using Game.UI.Collections;
using Game.UI.Elements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Universal.Behaviour;
using Universal.Core;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class MiniGameItem : ContextActionsItem<MiniGameInfo>
    {
        #region fields & properties
        [SerializeField] private Image preview;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image raycastImage;
        #endregion fields & properties

        #region methods
        protected override void UpdateUI()
        {
            base.UpdateUI();
            IReadOnlyList<int> completedMiniGames = GameData.Data.MiniGamesData.CompletedMiniGames;
            bool isPreviousMiniGameCompleted = Context.PreviousMiniGame == null || completedMiniGames.Exists(x => x == Context.PreviousMiniGame.Id, out _);
            preview.sprite = isPreviousMiniGameCompleted ? Context.PreviewSprite : Context.LockedSprite;
            raycastImage.enabled = isPreviousMiniGameCompleted;
            nameText.text = Context.NameInfo.Text;
        }
        [Todo("Send mini game to buffer")]
        public void LoadMiniGame()
        {
            //mini games instance.game = ...
            SceneLoader.LoadScene("Game");
        }
        #endregion methods
    }
}