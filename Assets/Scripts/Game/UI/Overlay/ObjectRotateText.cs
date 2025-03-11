using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Animation;
using Game.UI.Elements;

namespace Game.UI.Overlay
{
    public class ObjectRotateText : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private ObjectRotate objectRotate;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private ProgressBar.ProgressTextFormat textFormat;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            objectRotate.OnRotateEnd += UpdateUI;
        }
        private void OnDisable()
        {
            objectRotate.OnRotateEnd -= UpdateUI;
        }
        private void UpdateUI()
        {
            countText.text = ProgressBar.GetText(textFormat, objectRotate.CurrentRotationId + 1, 0, objectRotate.Rotations.Count);
        }
        #endregion methods
    }
}