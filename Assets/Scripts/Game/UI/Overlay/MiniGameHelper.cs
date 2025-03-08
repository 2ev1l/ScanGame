using EditorCustom.Attributes;
using Game.DataBase;
using Game.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Universal.Time;

namespace Game.UI.Overlay
{
    public class MiniGameHelper : MonoBehaviour
    {
        #region fields & properties
        private static readonly LanguageInfo InfoHeader = new(32, TextType.Menu);
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image uiLock;
        private readonly TimeDelay timeDelay = new(1);
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            MiniGameLoader.OnMiniGameChanged += UpdateUI;
            UpdateUI();
        }
        private void OnDisable()
        {
            MiniGameLoader.OnMiniGameChanged -= UpdateUI;
            timeDelay.OnDelayReady = null;
        }
        private void UpdateUI(int _) => UpdateUI();
        private void UpdateUI()
        {
            timeDelay.OnDelayReady = null;
            LockUI();
            UpdateNameText();
            ShowInfoDelayed();
            UnlockUIDelayed();
            timeDelay.Activate();
        }
        private void UpdateNameText()
        {
            nameText.text = MiniGameLoader.LastInfo.NameInfo.Text;
        }
        private void ShowInfoDelayed()
        {
            timeDelay.OnDelayReady += ShowCurrentInfo;
        }
        private void LockUI()
        {
            uiLock.enabled = true;
        }
        private void UnlockUIDelayed()
        {
            timeDelay.OnDelayReady += UnlockUI;
        }
        private void UnlockUI()
        {
            uiLock.enabled = false;
        }

        [SerializedMethod]
        public void ShowCurrentInfoOrNull()
        {
            if (!CanShowCurrentInfo())
                new InfoRequest(null, InfoHeader, new List<HelpInfo>() { new(new LanguageInfo(33, TextType.Menu), null) }).Send();
            else
                SendHelpInfoRequest();
        }
        [SerializedMethod]
        public void ShowCurrentInfo()
        {
            if (!CanShowCurrentInfo()) return;
            SendHelpInfoRequest();
        }
        private void SendHelpInfoRequest()
        {
            MiniGameInfo info = MiniGameLoader.LastInfo;
            new InfoRequest(null, InfoHeader, info.HelpInfo.Data.Infos).Send();
        }
        private bool CanShowCurrentInfo()
        {
            return MiniGameLoader.LastInfo.HelpInfo != null;
        }
        #endregion methods
    }
}