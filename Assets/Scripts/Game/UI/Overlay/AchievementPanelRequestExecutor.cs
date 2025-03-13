using Game.Audio;
using Game.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Universal.Behaviour;
using Universal.Events;
using Universal.Time;

namespace Game.UI.Overlay
{
    public class AchievementPanelRequestExecutor : OverlayPanel<AchievementPanelRequest>
    {
        #region fields & properties
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image image;
        [SerializeField] private CanvasAlphaChanger alphaChanger;
        [SerializeField][Min(0.1f)] private float alphaChangeSpeed = 1f;
        [SerializeField][Min(0.1f)] private float awaitTime = 3f;
        [SerializeField] private AudioClip achievementSound;
        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            base.OnEnable();
            RequestController.EnableExecution(this);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            RequestController.DisableExecution(this);
        }
        protected override void UpdateUI(AchievementPanelRequest request)
        {
            base.UpdateUI(request);
            nameText.text = request.Info.NameInfo.Text;
            descriptionText.text = request.Info.DescriptionInfo.Text;
            image.sprite = request.Info.PreviewSprite;
            alphaChanger.OnCycleEnd = CloseUI;
            SingleGameInstance.Instance.StartCoroutine(alphaChanger.DoCycle(alphaChangeSpeed, awaitTime));
            AudioManager.PlayClip(achievementSound, Audio.AudioType.Sound);
        }
        #endregion methods
    }
}