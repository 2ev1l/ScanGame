using EditorCustom.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class PerspecitveScale : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Vector2 minMaxY = new(0, 100);
        [SerializeField] private Vector2 minMaxScale = new(1, 1.1f);
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void ApplyScale()
        {
            float y = rectTransform.anchoredPosition3D.y;
            float lerp = Mathf.InverseLerp(minMaxY.x, minMaxY.y, y);
            Vector3 scale = Vector3.one * Mathf.Lerp(minMaxScale.x, minMaxScale.y, lerp);
            rectTransform.localScale = scale;
        }
        #endregion methods
    }
}