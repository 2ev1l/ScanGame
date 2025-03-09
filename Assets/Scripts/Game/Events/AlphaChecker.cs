using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal.Core;

namespace Game.Events
{
    public class AlphaChecker : ResultChecker
    {
        #region fields & properties
        [SerializeField] private Image image;
        [SerializeField][Range(0, 1)] private float requiredAlpha = 1f;
        [SerializeField][Range(0, 1)] private float epsilon = 0.05f;
        #endregion fields & properties

        #region methods
        public override bool GetResult()
        {
            return image.color.a.Approximately(requiredAlpha, epsilon);
        }
        #endregion methods
    }
}