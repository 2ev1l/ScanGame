using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Core;

namespace Game.Events
{
    public class PositionChecker : ResultChecker
    {
        #region fields & properties
        [Title("Position")]
        [SerializeField] private Transform checkObject;
        [SerializeField] private bool checkLocalPosition = false;
        [SerializeField] private bool checkRectTransform = false;
        [SerializeField] private Vector3 requiredPosition = Vector3.zero;
        [SerializeField][Min(0)] private float epsilon = 0.1f;
        #endregion fields & properties

        #region methods
        public override bool GetResult()
        {
            float epsilon = Mathf.Max(this.epsilon * this.epsilon, this.epsilon);
            if (checkRectTransform)
            {
                RectTransform checkTransform = (RectTransform)checkObject;
                if (checkLocalPosition)
                {
                    return requiredPosition.Approximately(checkTransform.anchoredPosition3D, epsilon);
                }
                else
                {
                    return requiredPosition.Approximately(checkTransform.position, epsilon);
                }
            }
            else
            {
                if (checkLocalPosition)
                {
                    return requiredPosition.Approximately(checkObject.localPosition, epsilon);
                }
                else
                {
                    return requiredPosition.Approximately(checkObject.position, epsilon);
                }
            }
        }
        #endregion methods
    }
}