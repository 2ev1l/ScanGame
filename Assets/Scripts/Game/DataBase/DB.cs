using UnityEngine;
using EditorCustom.Attributes;
using Universal.Core;
using System.Linq;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

namespace Game.DataBase
{
    [ExecuteAlways]
    public class DB : MonoBehaviour
    {
        #region fields & properties
        public static DB Instance
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying) //only in editor
                    return GameObject.FindFirstObjectByType<DB>(FindObjectsInactive.Include);
#endif //UNITY_EDITOR
                return instance;
            }
            set
            {
                if (instance == null)
                    instance = value;
            }
        }
        private static DB instance;

        public DBSOSet<HelpInfosSO> HelpInfos => helpInfos;
        [SerializeField] private DBSOSet<HelpInfosSO> helpInfos;
        public DBSOSet<MiniGameInfoSO> MiniGames => miniGames;
        [SerializeField] private DBSOSet<MiniGameInfoSO> miniGames;
        public DBSOSet<MiniGameStageSO> MiniGameStages => miniGameStages;
        [SerializeField] private DBSOSet<MiniGameStageSO> miniGameStages;
        public DBSOSet<AchievementInfoSO> Achievements => achievements;
        [SerializeField] private DBSOSet<AchievementInfoSO> achievements;
        #region optimization

        #endregion optimization
        #endregion fields & properties

        #region methods

        #endregion methods

#if UNITY_EDITOR
        [SerializeField] private bool automaticallyUpdateEditor = false;
        private void OnValidate()
        {
            if (!automaticallyUpdateEditor) return;
            GetAllDBInfo();
            CheckAllErrors();
        }
        /// <summary>
        /// You need to manually add code
        /// </summary>
        [Button(nameof(GetAllDBInfo))]
        private void GetAllDBInfo()
        {
            if (Application.isPlaying) return;
            AssetDatabase.Refresh();
            Undo.RegisterCompleteObjectUndo(this, "Update DB");

            //call dbset.CollectAll()
            helpInfos.CollectAll();
            miniGames.CollectAll();
            miniGameStages.CollectAll();
            achievements.CollectAll();

            EditorUtility.SetDirty(this);
        }
        /// <summary>
        /// You need to manually add code
        /// </summary>
        [Button(nameof(CheckAllErrors))]
        private void CheckAllErrors()
        {
            if (!Application.isPlaying) return;
            //call dbset.CatchExceptions(x => ...)
            System.Exception e = new();

            helpInfos.CatchDefaultExceptions();
            helpInfos.CatchExceptions(x =>
            {
                foreach (var el in x.Data.Infos)
                {
                    _ = el.Text.Text;
                }
            }, "Wrong text in info");

            CatchNameHandlerExceptions<MiniGameInfoSO, MiniGameInfo>(miniGames);
            CatchPreviewSpriteHandlerExceptions<MiniGameInfoSO, MiniGameInfo>(miniGames);
            miniGames.CatchExceptions(x => x.Data.LockedSprite == null, e, "Locked sprite must not be null");
            
            CatchNameHandlerExceptions<MiniGameStageSO, MiniGameStage>(miniGameStages);

            CatchNameHandlerExceptions<AchievementInfoSO, AchievementInfo>(achievements);
            CatchDescriptionHandlerExceptions<AchievementInfoSO, AchievementInfo>(achievements);
            CatchPreviewSpriteHandlerExceptions<AchievementInfoSO, AchievementInfo>(achievements);
        }
        private void CatchNameHandlerExceptions<SO, Data>(DBSOSet<SO> dbset) where SO : DBScriptableObject<Data> where Data : DBInfo, INameHandler
        {
            dbset.CatchExceptions(x => _ = x.Data.NameInfo, "Name info is not correct");
        }
        private void CatchDescriptionHandlerExceptions<SO, Data>(DBSOSet<SO> dbset) where SO : DBScriptableObject<Data> where Data : DBInfo, IDescriptionHandler
        {
            dbset.CatchExceptions(x => _ = x.Data.DescriptionInfo, "Description info is not correct");
        }
        private void CatchPreviewSpriteHandlerExceptions<SO, Data>(DBSOSet<SO> dbset) where SO : DBScriptableObject<Data> where Data : DBInfo, IPreviewSpriteHandler
        {
            dbset.CatchExceptions(x => x.Data.PreviewSprite == null, new System.Exception(), "Preview sprite must not be null");
        }
#endif //UNITY_EDITOR
    }
}