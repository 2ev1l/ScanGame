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
        public class AchievementsTests
        {
            #region fields & properties
            private int counter = 0;
            #endregion fields & properties

            #region methods
            [Test]
            public void InitializeAchievementsDataTest()
            {
                PrepareAnyTest(out AchievementsData ad);
                Assert.AreEqual(0, ad.UnlockedAchievements.Count);
            }
            [Test]
            public void AddAchievementPositiveTest()
            {
                PrepareAnyTest(out AchievementsData ad);
                Assert.IsTrue(DB.Instance.Achievements.Data.Count >= 2);
                ad.OnAchievementUnlocked = IncreaseCounter;
                Assert.IsTrue(ad.TryUnlockAchievement(0));
                Assert.AreEqual(1, counter);
                Assert.AreEqual(1, ad.UnlockedAchievements.Count);
                Assert.IsTrue(ad.TryUnlockAchievement(1));
                Assert.AreEqual(2, counter);
                Assert.AreEqual(2, ad.UnlockedAchievements.Count);
            }
            [Test]
            public void AddAchievementNegativeTest()
            {
                PrepareAnyTest(out AchievementsData ad);
                Assert.IsTrue(DB.Instance.Achievements.Data.Count >= 2);
                ad.OnAchievementUnlocked = IncreaseCounter;
                Assert.IsFalse(ad.TryUnlockAchievement(-1));
                Assert.AreEqual(0, counter);
                Assert.AreEqual(0, ad.UnlockedAchievements.Count);
                Assert.IsFalse(ad.TryUnlockAchievement(DB.Instance.Achievements.Data.Count));
                Assert.AreEqual(0, counter);
                Assert.AreEqual(0, ad.UnlockedAchievements.Count);
            }
            private void IncreaseCounter(int _) => IncreaseCounter();
            private void IncreaseCounter() => counter++;
            private void PrepareAnyTest(out AchievementsData ad)
            {
                AssetLoader.InitInstances();
                GameData.SetData(new());
                ad = GameData.Data.AchievementsData;
            }
            #endregion methods
        }
    }
}