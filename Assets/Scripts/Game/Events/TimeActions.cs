using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class TimeActions : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private bool resetTimeScaleOnDisable = true;
        #endregion fields & properties

        #region methods
        private void OnDisable()
        {
            ResetTimeScale();
        }
        [SerializedMethod]
        public void SetTimeScale(float value)
        {
            Time.timeScale = value;
        }
        [SerializedMethod]
        public void ResetTimeScale()
        {
            Time.timeScale = 1;
        }
        #endregion methods
    }
}