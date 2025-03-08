using DebugStuff;
using EditorCustom.Attributes;
using Game.Events;
using Game.UI.Elements;
using TMPro;
using UnityEngine;
using Universal.Events;

namespace Game.UI.Overlay
{
    public class OverlayConfirmPanel : OverlayPanel<ConfirmRequest>
    {
        #region fields & properties
        public CustomButton ConfirmButton => confirmButton;
        [SerializeField] private CustomButton confirmButton;
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI mainText;
        private bool confirmState = false;
        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            base.OnEnable();
            confirmButton.OnClicked += CloseUI;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            confirmButton.OnClicked -= CloseUI;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            confirmButton.OnClicked = null;
            if (CurrentRequest != null)
                ExecuteRequest();
        }
        public override bool TryExecuteRequest(ExecutableRequest request)
        {
            if (CanExecuteRequest(request))
            {
                ConfirmRequest cr = (ConfirmRequest)request;
                OpenUI(cr);
                CurrentRequest = cr;
                CloseButton.OnClicked += SetRejectState;
                ConfirmButton.OnClicked += SetConfirmCtate;

                CloseButton.OnClicked += ExecuteRequest;
                ConfirmButton.OnClicked += ExecuteRequest;
                return true;
            }
            return false;
        }
        protected override void UpdateUI(ConfirmRequest request)
        {
            base.UpdateUI(request);
            headerText.text = request.HeaderInfo;
            mainText.text = request.MainInfo;
        }
        private void SetConfirmCtate()
        {
            confirmState = true;
        }
        private void SetRejectState()
        {
            confirmState = false;
        }

        protected override void ExecuteRequest()
        {
            CloseButton.OnClicked -= SetRejectState;
            ConfirmButton.OnClicked -= SetConfirmCtate;

            CloseButton.OnClicked -= ExecuteRequest;
            ConfirmButton.OnClicked -= ExecuteRequest;
            ConfirmRequest cr = (ConfirmRequest)CurrentRequest;
            if (confirmState)
                cr.Confirm();
            else
                cr.Reject();

            CurrentRequest = null;
        }
        #endregion methods

#if UNITY_EDITOR
        [Title("Tests")]
        [SerializeField][DontDraw] private bool ___testBool;
        [Button(nameof(TestShowPanel))]
        private void TestShowPanel()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            ConfirmRequest cr = new(null, null, new DataBase.LanguageInfo(1, DataBase.TextType.Menu), new(0, DataBase.TextType.Menu));
            cr.Send();
        }
#endif //UNITY_EDITOR

    }
}