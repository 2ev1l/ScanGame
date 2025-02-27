using System;
using Universal.Events;
using Game.DataBase;
using Game.UI.Text;
using EditorCustom.Attributes;
using System.Collections.Generic;

namespace Game.Events
{
    [System.Serializable]
    public class InfoRequest : ExecutableRequest
    {
        #region fields & properties
        public Action OnRejected;
        public string HeaderInfo;
        public IReadOnlyList<HelpInfo> HelpInfos;
        #endregion fields & properties

        #region methods
        public override void Close()
        {
            OnRejected?.Invoke();
        }
        public InfoRequest() { }
        public InfoRequest(Action onRejected, LanguageInfo headerInfo, IReadOnlyList<HelpInfo> HelpInfos)
        {
            this.OnRejected = onRejected;
            this.HeaderInfo = headerInfo.Text;
            this.HelpInfos = HelpInfos;
        }
        public InfoRequest(Action onRejected, string headerInfo, IReadOnlyList<HelpInfo> HelpInfos)
        {
            this.OnRejected = onRejected;
            this.HeaderInfo = headerInfo;
            this.HelpInfos = HelpInfos;
        }
        #endregion methods
    }
}