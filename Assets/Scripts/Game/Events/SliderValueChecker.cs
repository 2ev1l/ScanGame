using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal.Core;

namespace Game.Events
{
    public class SliderValueChecker : ResultChecker
    {
        #region fields & properties
        [SerializeField] private Slider slider;
        [SerializeField] private float myValue = 0;
        [SerializeField][Min(0f)] private float epsilon = 0.001f;
        #endregion fields & properties

        #region methods
        public override bool GetResult()
        {
            return slider.value.Approximately(myValue, epsilon);
        }
        #endregion methods
    }
}