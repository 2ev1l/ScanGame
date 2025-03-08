using EditorCustom.Attributes;
using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public class InfoTrigger : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private UnityEvent OnInfoClosed;
        [SerializeField] private LanguageInfo headerInfo = new(32, TextType.Menu);
        [SerializeField] private List<HelpInfo> helpInfos = new();
        #endregion fields & properties

        #region methods
        [SerializedMethod]
        public void ShowInfo()
        {
            new InfoRequest(OnClosedInfo, headerInfo, helpInfos).Send();
        }
        private void OnClosedInfo()
        {
            OnInfoClosed?.Invoke();
        }
        #endregion methods
    }
}