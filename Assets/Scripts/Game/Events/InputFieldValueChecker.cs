using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Universal.Core;

namespace Game.Events
{
    public class InputFieldValueChecker : ResultChecker
    {
        #region fields & properties
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private string myValue = "";
        [SerializeField][Min(0f)] private float epsilon = 0.001f;
        #endregion fields & properties

        #region methods
        public override bool GetResult()
        {
            return IsTypeEqual(inputField.contentType, myValue, inputField.text, epsilon);
        }
        private static bool IsTypeEqual(TMP_InputField.ContentType type, string myValue, string inputValue, float e = 0.001f) => type switch
        {
            TMP_InputField.ContentType.IntegerNumber => CheckInt(myValue, inputValue, e),
            TMP_InputField.ContentType.DecimalNumber => CheckDecimal(myValue, inputValue, e),
            _ => myValue.Equals(inputValue),
        };
        private static bool CheckInt(string myValue, string inputValue, float e)
        {
            int myValueI = 0;
            int inputValueI = 0;
            try { myValueI = System.Convert.ToInt32(myValue); }
            catch { }
            try { inputValueI = System.Convert.ToInt32(inputValue); }
            catch { }
            return myValueI.Approximately(inputValueI, e);
        }
        private static bool CheckDecimal(string myValue, string inputValue, float e)
        {
            float myValueD = 0;
            float inputValueD = 0;
            try { myValueD = CustomMath.ConvertToFloat(myValue); }
            catch { }
            try { inputValueD = CustomMath.ConvertToFloat(inputValue); }
            catch { }
            return myValueD.Approximately(inputValueD, e);
        }
        #endregion methods
    }
}