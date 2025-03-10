using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Graphics
{
    [RequireComponent(typeof(Image))]
    [ExecuteAlways]
    public class DepthOfFieldActions : MonoBehaviour
    {
        #region fields & properties
        [Title("Focus Settings")]
        [Range(0f, 1f)] public float focusRange = 0.2f;
        [Range(0f, 0.1f)] public float maxBlur = 0.05f;
        [Range(0f, 1f)] public float smoothness = 0.5f;
        public Vector2 focusPoint = new(0.5f, 0.5f);

        private Material dofMaterial;
        private Image targetImage;
        private RectTransform rectTransform;
        #endregion fields & properties

        #region methods
        private void Start()
        {
            targetImage = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
            if (dofMaterial != null)
                Destroy(dofMaterial);
            dofMaterial = Instantiate(targetImage.material);
            targetImage.material = dofMaterial;
            UpdateShaderProperties();
        }
        public void SetFocusRange(float value)
        {
            value = Mathf.Clamp01(value);
            focusRange = value;
            UpdateShaderProperties();
        }
        public void SetMaxBlur(float value)
        {
            value = Mathf.Clamp(value, 0, 0.1f);
            maxBlur = value;
            UpdateShaderProperties();
        }
        public void SetSmoothness(float value)
        {
            value = Mathf.Clamp01(value);
            smoothness = value;
            UpdateShaderProperties();
        }
        public void SetFocusPoint(Vector2 newFocusPoint)
        {
            focusPoint = newFocusPoint;
            UpdateShaderProperties();
        }
        private void UpdateShaderProperties()
        {
            if (dofMaterial == null) return;

            float angle = rectTransform.eulerAngles.z * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);
            Vector4 rotationMatrix = new Vector4(
                cos,
                -sin,
                sin,
                cos
            );

            dofMaterial.SetVector("_RotationMatrix", rotationMatrix);
            dofMaterial.SetVector("_FocusPoint", focusPoint);
            dofMaterial.SetFloat("_FocusRange", focusRange);
            dofMaterial.SetFloat("_MaxBlur", maxBlur);
            dofMaterial.SetFloat("_Smoothness", smoothness);
        }
        #endregion methods
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (targetImage != null && targetImage.material != dofMaterial)
                targetImage.material = dofMaterial;
            UpdateShaderProperties();
        }
        [Button(nameof(ClearData))]
        private void ClearData()
        {
            Start();
        }
#endif //UNITY_EDITOR
    }
}