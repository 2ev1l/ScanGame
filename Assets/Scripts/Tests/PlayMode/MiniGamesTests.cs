using System.Collections;
using System.Collections.Generic;
using Game.DataBase;
using Game.Serialization.World;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Universal.Events;

namespace Tests.PlayMode
{
    public partial class WorldDataTests
    {
        public class MiniGamesTests
        {
            #region fields & properties
            private int counter = 0;
            #endregion fields & properties

            #region methods
            [Test]
            public void InitializeMiniGamesDataTest()
            {
                PrepareAnyTest(out MiniGamesData gd);
                Assert.AreEqual(0, gd.CompletedMiniGames.Count);
            }
            [Test]
            public void AddCompletedMiniGamePositiveTest()
            {
                PrepareAnyTest(out MiniGamesData gd);
                Assert.IsTrue(DB.Instance.MiniGames.Data.Count >= 2);
                gd.OnMiniGameCompleted = IncreaseCounter;
                gd.AddCompletedMiniGame(0);
                Assert.AreEqual(1, counter);
                Assert.AreEqual(1, gd.CompletedMiniGames.Count);
                gd.AddCompletedMiniGame(1);
                Assert.AreEqual(2, gd.CompletedMiniGames.Count);
            }
            [Test]
            public void AddCompletedMiniGameNegativeTest()
            {
                PrepareAnyTest(out MiniGamesData gd);
                Assert.IsTrue(DB.Instance.MiniGames.Data.Count >= 2);
                gd.OnMiniGameCompleted = IncreaseCounter;
                gd.AddCompletedMiniGame(-1);
                Assert.AreEqual(0, counter);
                Assert.AreEqual(0, gd.CompletedMiniGames.Count);
                gd.AddCompletedMiniGame(DB.Instance.MiniGames.Data.Count);
                Assert.AreEqual(0, counter);
                Assert.AreEqual(0, gd.CompletedMiniGames.Count);
            }

            private void IncreaseCounter(int _) => IncreaseCounter();
            private void IncreaseCounter() => counter++;
            private void PrepareAnyTest(out MiniGamesData gd)
            {
                AssetLoader.InitInstances();
                GameData.SetData(new());
                gd = GameData.Data.MiniGamesData;
                counter = 0;
            }
            #endregion methods

        }
    }
}
