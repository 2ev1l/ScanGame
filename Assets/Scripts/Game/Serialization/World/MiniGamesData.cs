using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Universal.Collections.Generic;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class MiniGamesData
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> - Mini Game Id
        /// </summary>
        public UnityAction<int> OnMiniGameCompleted;
        /// <summary>
        /// <see cref="{T0}"/> - Mini Game Id
        /// </summary>
        public UnityAction<int> OnMiniGameChanged;
        public IReadOnlyList<int> CompletedMiniGames => completedMiniGames.Items;
        [SerializeField] private UniqueList<int> completedMiniGames = new();
        public int LastGame
        {
            get => lastGame;
            set => SetLastGame(value);
        }
        [System.NonSerialized] private int lastGame = 0;
        #endregion fields & properties

        #region methods
        public void AddCompletedMiniGame(int id)
        {
            if (!completedMiniGames.TryAddItem(id, x => x == id))
                return;
            OnMiniGameCompleted?.Invoke(id);
        }
        private void SetLastGame(int value)
        {
            lastGame = value;
            OnMiniGameChanged?.Invoke(value);
        }
        #endregion methods
    }
}