using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    [System.Serializable]
    public class DescriptionPanelRequest : AdditionalPanelRequest
    {
        #region fields & properties
        public string Description => description;
        private string description;
        #endregion fields & properties

        #region methods
        public DescriptionPanelRequest(bool open, string text) : base(open)
        {
            description = text;
        }
        public DescriptionPanelRequest(bool open, LanguageInfo languageInfo) : base(open)
        {
            description = languageInfo.Text;
        }
        #endregion methods
    }
}