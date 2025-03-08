using EditorCustom.Attributes;
using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI.Overlay
{
    public class MiniGameLoader : MonoBehaviour
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> - Mini Game Id
        /// </summary>
        public static UnityAction<int> OnMiniGameChanged;
        public static MiniGameInfo LastInfo => DB.Instance.MiniGames[LastGame].Data;
        public static int LastGame
        {
            get => lastGame;
            set => SetLastGame(value);
        }
        private static int lastGame = 0;
        #endregion fields & properties

        #region methods
        private static void SetLastGame(int value)
        {
            lastGame = value;
            OnMiniGameChanged?.Invoke(value);
        }
        [SerializedMethod]
        public void LoadNextGame()
        {
            CompleteGame();
            int nextGame = lastGame + 1;
            try
            {
                _ = LastInfo;
            }
            catch
            {
                return;
            }
            LastGame = nextGame;
        }
        [SerializedMethod]
        public void CompleteGame()
        {
            GameData.Data.MiniGamesData.AddCompletedMiniGame(lastGame);
        }

        #endregion methods
    }
}