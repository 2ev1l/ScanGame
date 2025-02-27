using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class AchievementInfo : DBInfo, INameHandler, IDescriptionHandler, IPreviewSpriteHandler
    {
        #region fields & properties
        public LanguageInfo NameInfo => nameInfo;
        [SerializeField] private LanguageInfo nameInfo = new(0, TextType.Game);
        public LanguageInfo DescriptionInfo => descriptionInfo;
        [SerializeField] private LanguageInfo descriptionInfo = new(0, TextType.Game);
        public Sprite PreviewSprite => previewSprite;
        [SerializeField] private Sprite previewSprite;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}