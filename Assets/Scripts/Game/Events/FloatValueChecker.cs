using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Core;

namespace Game.Events
{
    public class FloatValueChecker : ResultChecker
    {
        #region fields & properties
        [SerializeField] private FloatActions floatActions;
        [SerializeField] private float requiredValue;
        [SerializeField][Min(0)] private float epsilon = 0.01f;
        #endregion fields & properties

        #region methods
        public override bool GetResult()
        {
            return floatActions.Value.Approximately(requiredValue, epsilon);
        }
        #endregion methods
    }
}