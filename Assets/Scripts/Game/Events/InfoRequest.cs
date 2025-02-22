using System;
using Universal.Events;
using Game.DataBase;
using Game.UI.Text;
using EditorCustom.Attributes;

namespace Game.Events
{
    [System.Serializable]
    public class InfoRequest : ExecutableRequest
    {
        #region fields & properties
        public Action OnRejected;
        public string HeaderInfo;
        public string MainInfo;
        #endregion fields & properties

        #region methods
        [Todo]
        /// <summary>
        ///  <br></br>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static InfoRequest GetErrorRequest(int errorCode)
        {
            InfoRequest request = new(null, new LanguageInfo(-1, TextType.None).Text, $"{new LanguageInfo(0, TextType.Menu).Text}\n[{errorCode}]");
            return request;
        }
        public override void Close()
        {
            OnRejected?.Invoke();
        }
        public InfoRequest() { }
        public InfoRequest(Action onRejected, LanguageInfo headerInfo, LanguageInfo mainInfo)
        {
            this.OnRejected = onRejected;
            this.HeaderInfo = headerInfo.Text;
            this.MainInfo = mainInfo.Text;
        }
        public InfoRequest(Action onRejected, string headerInfo, string mainInfo)
        {
            this.OnRejected = onRejected;
            this.HeaderInfo = headerInfo;
            this.MainInfo = mainInfo;
        }
        #endregion methods
    }
}