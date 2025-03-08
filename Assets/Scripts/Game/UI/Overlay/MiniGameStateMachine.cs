using DebugStuff;
using EditorCustom.Attributes;
using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Universal.Behaviour;
using Universal.Core;

namespace Game.UI.Overlay
{
    public class MiniGameStateMachine : DefaultStateMachine
    {
        #region fields & properties
        #endregion fields & properties

        #region methods
        protected override void OnEnable()
        {
            base.OnEnable();
            MiniGameLoader.OnMiniGameChanged += ChangeCurrentGame;
#if UNITY_EDITOR
            if (loadInEditor)
            {
                MiniGameLoader.LastGame = gameToSet;
                Debug.Log("Loaded from debugger");
            }
#endif
            ChangeCurrentGame(MiniGameLoader.LastGame);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            MiniGameLoader.OnMiniGameChanged -= ChangeCurrentGame;
        }
        private void ChangeCurrentGame(int value)
        {
            foreach (var state in Context.States.Cast<MiniGameState>())
            {
                if (state.GameInfo.Id != value) continue;
                Context.ApplyState(state);
                return;
            }
            Debug.LogError($"Game #{value} was not found");
            Context.ApplyDefaultState();
        }
        #endregion methods


#if UNITY_EDITOR
        [Title("Tests")]
        [SerializeField][DontDraw] private bool ___testBool;
        [SerializeField][Min(0)] private int gameToSet = 0;
        [SerializeField] private bool loadInEditor = false;
        [Button(nameof(TestSetGame))]
        private void TestSetGame()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            MiniGameLoader.LastGame = gameToSet;
        }
        [Button(nameof(CheckGames))]
        private void CheckGames()
        {
            if (Context.States.ExistsEquals((x, y) => ((MiniGameState)x).GameInfo.Id == ((MiniGameState)y).GameInfo.Id))
            {
                Debug.LogError("Has equals mini games");
            }
            int states = Context.States.Count;
            int db = DB.Instance.MiniGames.Data.Count;
            if (states > db)
            {
                Debug.LogError($"States is more than mini games ({states} / {db})");
            }
            if (states < db)
            {
                Debug.LogError($"States is less than mini games ({states} / {db})");
            }
        }

#endif //UNITY_EDITOR
    }
}