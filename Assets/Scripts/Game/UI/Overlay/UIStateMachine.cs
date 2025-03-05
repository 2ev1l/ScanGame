using Game.Events;
using Game.Serialization.Settings.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Universal.Behaviour;
using Zenject;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class UIStateMachine : Observer
    {
        #region fields & properties
        [SerializeField] private StateMachine context;
        [SerializeField] private StateChange settingsState;
        private bool playerInputWasDisabled = false;
        private bool uiInputWasDisabled = false;
        #endregion fields & properties

        #region methods
        public override void Dispose()
        {
            InputController.OnKeyDown -= CheckDownKey;
            context.OnStateChanged -= CheckStateChanged;
            EnablePlayerInput();
            EnableUIInput();
        }
        public override void Initialize()
        {
            InputController.OnKeyDown += CheckDownKey;
            context.OnStateChanged += CheckStateChanged;
            CheckStateChanged(context.CurrentState);
        }

        private void CheckStateChanged(StateChange newState)
        {
            if (newState == settingsState)
            {
                DisableUIInput();
                DisablePlayerInput();
            }
        }
        private void CheckDownKey(KeyCodeInfo info)
        {
            if (info.Description.Equals(KeyCodeDescription.OpenSettings))
            {
                if (context.CurrentState == settingsState) context.ApplyDefaultState();
                else context.ApplyState(settingsState);
                return;
            }
            if (info.Description.Equals(((UIStateChange)context.CurrentState).CloseKey))
            {
                context.ApplyDefaultState();
                return;
            }
            ApplyStateByKeyCode(info.Description);
        }
        private void ApplyStateByKeyCode(KeyCodeDescription description)
        {
            foreach (UIStateChange state in context.States.Cast<UIStateChange>())
            {
                if (!state.OpenKey.Equals(description)) continue;
                context.ApplyState(state);
                return;
            }
        }
        private void DisableUIInput()
        {
            if (uiInputWasDisabled) return;
            InputController.LockUIInput(1);
            uiInputWasDisabled = true;
        }
        private void EnableUIInput()
        {
            if (!uiInputWasDisabled) return;
            InputController.UnlockUIInput(1);
            uiInputWasDisabled = false;
        }

        private void DisablePlayerInput()
        {
            if (playerInputWasDisabled) return;
            InputController.LockPlayerInput(1);
            playerInputWasDisabled = true;
        }
        private void EnablePlayerInput()
        {
            if (!playerInputWasDisabled) return;
            InputController.UnlockPlayerInput(1);
            playerInputWasDisabled = false;
        }
        #endregion methods
    }
}