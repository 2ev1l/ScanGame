using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class ObjectStateChecker : ResultChecker
    {
        #region fields & properties
        [SerializeField] private GameObject objectToCheck;
        [SerializeField] private bool mustBeEnabled = true;
        #endregion fields & properties

        #region methods
        public override bool GetResult()
        {
            if (mustBeEnabled)
            {
                return objectToCheck.activeSelf;
            }
            else
            {
                return !objectToCheck.activeSelf;
            }
        }
        #endregion methods
    }
}