using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Core;

namespace Game.Events
{
    public class RotationChecker : ResultChecker
    {
        #region fields & properties
        [Title("Rotation")]
        [SerializeField] private Transform checkObject;
        [SerializeField] private bool checkLocalRotation = false;
        [SerializeField] private Vector3 requiredEulerAngles = Vector3.zero;
        [SerializeField][Min(0)] private float epsilon = 0.1f;
        #endregion fields & properties

        #region methods
        public override bool GetResult()
        {
            Vector3 clampedRequiredAngles = requiredEulerAngles.ClampEulerAngles();
            if (checkLocalRotation)
            {
                return clampedRequiredAngles.ApproximatelyEulerAngles(checkObject.localEulerAngles, epsilon);
            }
            else
            {
                return clampedRequiredAngles.ApproximatelyEulerAngles(checkObject.eulerAngles, epsilon);
            }
        }
        #endregion methods
    }
}