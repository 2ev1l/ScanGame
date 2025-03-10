using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class FloatActions : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private UnityEvent<float> ResultChange;
        [SerializeField] private UnityEvent Operations;
        [SerializeField] private float value = 0f;
        #endregion fields & properties

        #region methods
        public void UpdateValue(float value)
        {
            this.value = value;
            Operations?.Invoke();
            InvokeResultChange();
        }
        public void InvokeResultChange() => ResultChange?.Invoke(this.value);
        public void OneMinus()
        {
            this.value = 1 - value;
        }
        public void OneMinusABS()
        {
            this.value = 1 - Mathf.Abs(value);
        }
        public void Scale(float scale)
        {
            this.value *= scale;
        }
        public void AddDeltaTime()
        {
            this.value += Time.deltaTime;
        }
        public void Add(float value)
        {
            this.value += value;
        }
        public void Power(float power)
        {
            this.value = Mathf.Pow(this.value, power);
        }
        public void Clamp01()
        {
            this.value = Mathf.Clamp01(this.value);
        }
        #endregion methods
    }
}