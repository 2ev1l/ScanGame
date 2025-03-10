using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Events;

namespace Game.Events
{
    public class MoveWithCursor : MonoBehaviour, IUpdateSender
    {
        #region fields & properties
        [SerializeField] private RectTransform movedObject;
        [SerializeField] private RectTransform parentObject;
        [SerializeField] private Canvas mainCanvas;
        private Vector3 startPosition;
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void StartMove()
        {
            MessageController.AddSender(this);
            startPosition = movedObject.localPosition;
        }
        [SerializedMethod]
        public void EndMove()
        {
            MessageController.RemoveSender(this);
        }
        [SerializedMethod]
        public void ReturnToStart()
        {
            movedObject.localPosition = startPosition;
        }
        public void UpdateMessage()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentObject, Input.mousePosition, mainCanvas.worldCamera, out Vector2 point);
            movedObject.localPosition = point;
        }
        #endregion methods
    }
}