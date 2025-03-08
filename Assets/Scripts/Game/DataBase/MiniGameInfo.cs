using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class MiniGameInfo : DBInfo, INameHandler, IPreviewSpriteHandler
    {
        #region fields & properties
        public LanguageInfo NameInfo => nameInfo;
        [SerializeField] private LanguageInfo nameInfo = new(0, TextType.Game);
        public HelpInfosSO HelpInfo => helpInfo;
        [SerializeField] private HelpInfosSO helpInfo = null;
        public Sprite PreviewSprite => previewSprite;
        [SerializeField] private Sprite previewSprite;
        public Sprite LockedSprite => lockedSprite;
        [SerializeField] private Sprite lockedSprite;
        public MiniGameInfoSO PreviousMiniGame => previousMiniGame;
        [SerializeField] private MiniGameInfoSO previousMiniGame;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}