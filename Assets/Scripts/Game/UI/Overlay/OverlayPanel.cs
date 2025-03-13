using Game.Events;
using Game.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Universal.Events;

namespace Game.UI.Overlay
{
    //Controlled by overlay panels controller
    public abstract class OverlayPanel<T> : OverlayPanelBase where T : ExecutableRequest
    {
        #region fields & properties
        public CustomButton CloseButton => closeButton;
        [SerializeField] private CustomButton closeButton;
        protected T CurrentRequest
        {
            get => currentRequest;
            set => currentRequest = value;
        }
        private T currentRequest = null;
        #endregion fields & properties

        #region methods
        protected virtual void OnEnable()
        {
            if (CloseButton != null)
                CloseButton.OnClicked += CloseUI;
        }
        protected virtual void OnDisable()
        {
            if (CloseButton != null)
                CloseButton.OnClicked -= CloseUI;
        }
        protected virtual void OnDestroy()
        {
            if (CloseButton != null)
                CloseButton.OnClicked = null;
        }
        public virtual bool CanExecuteRequest(ExecutableRequest request)
        {
            return request.GetType() == typeof(T);
        }
        public override bool TryExecuteRequest(ExecutableRequest request)
        {
            if (CanExecuteRequest(request))
            {
                CurrentRequest = (T)request;
                OpenUI(CurrentRequest);
                if (CloseButton != null)
                    CloseButton.OnClicked += ExecuteRequest;
                return true;
            }
            return false;
        }
        protected virtual void ExecuteRequest()
        {
            if (CloseButton != null)
                CloseButton.OnClicked -= ExecuteRequest;
            currentRequest.Close();
        }
        public void OpenUI(T request)
        {
            Panel.SetActive(true);
            UpdateUI(request);
        }
        protected virtual void UpdateUI(T request) { }
        public void CloseUI()
        {
            Panel.SetActive(false);
            OnPanelClosed?.Invoke();
        }

        #endregion methods
    }
}