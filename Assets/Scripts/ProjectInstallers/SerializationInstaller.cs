using EditorCustom.Attributes;
using Game.DataBase;
using Game.Serialization;
using Game.UI.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Behaviour;
using Universal.Events;
using Zenject;

namespace ProjectInstallers
{
    public class SerializationInstaller : MonoInstaller
    {
        #region fields & properties
        [SerializeField] private DB dataBase;
        [SerializeField] private SavingController savingController;
        [SerializeField] private TextData textData;
        [SerializeField] private TextObserver textObserver;
        #endregion fields & properties

        #region methods
        public override void InstallBindings()
        {
            Debug.Log("I");
            InstallDB();
            InstallSavingController();
            InstallTextData();
            InstallTextObserver();
        }
        private void InstallTextObserver()
        {
            Container.BindInterfacesAndSelfTo<TextObserver>().FromInstance(textObserver).AsSingle().NonLazy();
            textObserver.LoadChoosedLanguage();
        }
        private void InstallSavingController()
        {
            Container.Bind<SavingController>().FromInstance(savingController).AsSingle().NonLazy();
            savingController.ForceInitialize();
        }
        private void InstallTextData()
        {
            Container.Bind<TextData>().FromInstance(textData).AsSingle().NonLazy();
        }
        private void InstallDB()
        {
            Container.Bind<DB>().FromInstance(dataBase).AsSingle().NonLazy();
            DB.Instance = dataBase;
        }
        #endregion methods
#if UNITY_EDITOR
        [Title("Debug")]
        [SerializeField] private LanguageEditor testLanguage;
        [Button(nameof(SetLanguage))]
        private void SetLanguage()
        {
            if (!Application.isPlaying) return;
            textObserver.LoadLanguage(GetLanguageEditorName(testLanguage));
            textObserver.UpdateTextObjects();
        }
        private enum LanguageEditor
        {
            Russian,
        }
        private string GetLanguageEditorName(LanguageEditor e) => e switch
        {
            LanguageEditor.Russian => "Russian Русский",

            _ => throw new System.NotImplementedException(e.ToString()),
        };
#endif //UNITY_EDITOR
    }
}