using System;
using Game.DataBase;
using Game.UI.Text;
using Universal.Events;

namespace Game.Events
{
    [System.Serializable]
    public sealed class ConfirmRequest : ExecutableRequest
    {
        #region fields & properties
        public Action OnRejected;
        public Action OnConfirmed;
        public string MainInfo;
        public string HeaderInfo;
        private bool isConfirmed;
        #endregion fields & properties

        #region methods
        public void Confirm()
        {
            isConfirmed = true;
            Close();
        }
        public void Reject()
        {
            isConfirmed = false;
            Close();
        }
        /// <summary>
        /// Call <see cref="Confirm"/> or <see cref="Reject"/> instead
        /// </summary>
        public override void Close()
        {
            if (isConfirmed) OnConfirmed?.Invoke();
            else OnRejected?.Invoke();
        }
        public ConfirmRequest() { }
        public ConfirmRequest(Action onConfirmed, Action onRejected, LanguageInfo headerInfo, LanguageInfo mainInfo)
        {
            this.OnConfirmed = onConfirmed;
            this.OnRejected = onRejected;
            this.HeaderInfo = headerInfo.Text;
            this.MainInfo = mainInfo.Text;
        }
        public ConfirmRequest(Action onConfirmed, Action onRejected, string headerInfo, string mainInfo)
        {
            this.OnConfirmed = onConfirmed;
            this.OnRejected = onRejected;
            this.HeaderInfo = headerInfo;
            this.MainInfo = mainInfo;
        }
        #endregion methods
    }
}