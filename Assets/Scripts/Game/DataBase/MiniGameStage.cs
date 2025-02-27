using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class MiniGameStage : DBInfo, INameHandler
    {
        #region fields & properties
        public LanguageInfo NameInfo => nameInfo;
        [SerializeField] private LanguageInfo nameInfo = new(0, TextType.Game);
        public IReadOnlyList<MiniGameInfoSO> MiniGames => miniGames;
        [SerializeField] private List<MiniGameInfoSO> miniGames = new();
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}