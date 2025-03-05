using DebugStuff;
using EditorCustom.Attributes;
using Game.Events;
using Game.Serialization.World;
using Game.UI.Overlay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Behaviour;
using Zenject;

namespace Game.Installers
{
    public class ObserversInstaller : MonoInstaller
    {
        #region fields & properties
        [SerializeField] private UIStateMachine uiStateMachine;
        #endregion fields & properties

        #region methods
        public override void InstallBindings()
        {
            BindObserverFromInstance(uiStateMachine);
        }
        private void BindObserverFromInstance<T>(T instance) where T : Observer
        {
            Container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle();
            Container.QueueForInject(instance);
        }
        #endregion methods

    }
}