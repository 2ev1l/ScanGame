using EditorCustom.Attributes;
using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class ConfirmTrigger : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private UnityEvent OnRejected;
        [SerializeField] private UnityEvent OnConfirmed;
        [SerializeField] private LanguageInfo headerInfo = new(28, TextType.Menu);
        [SerializeField] private LanguageInfo mainInfo = new(0, TextType.Game);
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void ShowConfirm()
        {
            new ConfirmRequest(Confirm, Reject, headerInfo, mainInfo).Send();
        }
        private void Confirm()
        {
            OnConfirmed?.Invoke();
        }
        private void Reject()
        {
            OnRejected?.Invoke();
        }
        #endregion methods
    }
}