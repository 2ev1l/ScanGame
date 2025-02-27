using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class ObjectStateEvents : MonoBehaviour
    {
        #region fields & properties
        public UnityEvent OnEnableEvent;
        public UnityEvent OnDisableEvent;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            OnEnableEvent?.Invoke();
        }
        private void OnDisable()
        {
            OnDisableEvent?.Invoke();
        }
        #endregion methods
    }
}