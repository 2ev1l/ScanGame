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

        public DBSOSet<HelpInfoSO> HelpInfo => helpInfo;
        [SerializeField] private DBSOSet<HelpInfoSO> helpInfo;
        public DBSOSet<MiniGameInfoSO> MiniGames => miniGames;
        [SerializeField] private DBSOSet<MiniGameInfoSO> miniGames;
        public DBSOSet<MiniGameStageSO> MiniGameStages => miniGameStages;
        [SerializeField] private DBSOSet<MiniGameStageSO> miniGameStages;
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
            helpInfo.CollectAll();
            miniGames.CollectAll();
            miniGameStages.CollectAll();

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

            helpInfo.CatchDefaultExceptions();

            CatchNameHandlerExceptions<MiniGameInfoSO, MiniGameInfo>(miniGames);
            miniGames.CatchExceptions(x => x.Data.PreviewSprite == null, e, "Preview sprite must not be null");
            miniGames.CatchExceptions(x => x.Data.LockedSprite == null, e, "Locked sprite must not be null");

            CatchNameHandlerExceptions<MiniGameStageSO, MiniGameStage>(miniGameStages);
        }
        private void CatchNameHandlerExceptions<SO, Data>(DBSOSet<SO> dbset) where SO : DBScriptableObject<Data> where Data : DBInfo, INameHandler
        {
            dbset.CatchExceptions(x => _ = x.Data.NameInfo, "Name info is not correct");
        }
#endif //UNITY_EDITOR
    }
}