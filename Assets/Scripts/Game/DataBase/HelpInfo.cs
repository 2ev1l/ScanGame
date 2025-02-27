using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class HelpInfo: DBInfo
    {
        #region fields & properties
        public LanguageInfo Text => text;
        [SerializeField] private LanguageInfo text = new(0, TextType.Menu);
        public Sprite Sprite => sprite;
        [SerializeField] private Sprite sprite = null;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}