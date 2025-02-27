using Game.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Core;
using Universal.Events;
using Zenject;

namespace Game.UI.Overlay
{
    public class AdditionalPanelRequestExecutor : RequestExecutorBehaviour, IMessageSender, IUpdateSender
    {
        #region fields & properties
        [SerializeField] private RectTransform panel;
        [SerializeField] private RectTransform panelParent;
        [SerializeField] private bool updatePositionConstantly = false;
        #endregion fields & properties

        #region methods
        public override bool TryExecuteRequest(ExecutableRequest request)
        {
            if (request is not AdditionalPanelRequest panelRequest) return false;
            if (!CanExecuteAdditionalPanelRequest(panelRequest)) return false;

            if (panelRequest.Open)
            {
                OpenPanel();
                UpdatePanelUI(panelRequest);
            }
            else ClosePanel();

            return true;
        }
        protected virtual bool CanExecuteAdditionalPanelRequest(AdditionalPanelRequest request) => true;
        protected virtual void UpdatePanelUI(AdditionalPanelRequest request) { }
        protected void OpenPanel()
        {
            panel.gameObject.SetActive(true);
            UpdatePosition();
            if (updatePositionConstantly) MessageController.AddSender(this);
        }
        protected void ClosePanel()
        {
            panel.gameObject.SetActive(false);
            MessageController.RemoveSender(this);
        }
        private void UpdatePosition()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelParent, Input.mousePosition, CanvasInitializer.OverlayCamera, out Vector2 localPoint);
            Vector2 square = CustomMath.GetScreenSquare();
            panel.transform.localPosition = new Vector3(localPoint.x + square.x * (panel.rect.width / 2), localPoint.y + square.y * (panel.rect.height / 2));
        }

        public void UpdateMessage()
        {
            UpdatePosition();
        }
        #endregion methods
    }
}