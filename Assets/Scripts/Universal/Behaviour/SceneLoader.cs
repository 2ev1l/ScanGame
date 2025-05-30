using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Universal.Core;
using Universal.Serialization;

namespace Universal.Behaviour
{
    public static class SceneLoader
    {
        #region fields & properties
        public static UnityAction OnSceneLoaded;
        /// <summary>
        /// <see cref="{T0}"/> loadingPercent;
        /// </summary>
        public static UnityAction<float> OnSceneLoading;
        /// <summary>
        /// <see cref="{T0}"/> oldSceneName;
        /// <see cref="{T1}"/> newSceneName;
        /// </summary>
        public static UnityAction<string, string> OnSceneChanged;
        /// <summary>
        /// <see cref="{T0}"/> offsetTime
        /// </summary>
        public static UnityAction<float> OnStartLoading;
        public static bool IsSceneLoading { get; private set; }
        public static float LoadingDefaultTime { get; private set; } = 0.5f;
        public static Scene CurrentScene => SceneManager.GetActiveScene();
        private static string SceneToLoad;
        #endregion fields & properties

        #region methods
        public static void LoadScene(string scene) => LoadScene(scene, LoadingDefaultTime);
        public static void LoadScene(string scene, float time)
        {
            OnStartLoading?.Invoke(time);
            IsSceneLoading = true;
            SceneToLoad = scene;
            RemoveEvents();
            SingleGameInstance.Instance.StartCoroutine(LoadSceneIEnumerator(SceneToLoad, time));
        }
        private static IEnumerator LoadSceneIEnumerator(string scene, float timeToAwait)
        {
            yield return new WaitForSeconds(timeToAwait);
            string oldScene = SceneManager.GetActiveScene().name;
            IsSceneLoading = true;
            if (SavingUtils.Instance.CanSave())
                SavingUtils.Instance.SaveGameData();

            AsyncOperation newSceneLoad = SceneManager.LoadSceneAsync(scene);
            while (!newSceneLoad.isDone)
            {
                OnSceneLoading?.Invoke(newSceneLoad.progress);
                yield return CustomMath.WaitAFrame();
            }

            IsSceneLoading = false;
            OnSceneLoaded?.Invoke();
            OnSceneChanged?.Invoke(oldScene, scene);
        }
        private static void RemoveEvents()
        {
            GameObject eventSystem = GameObject.Find("EventSystem");
            if (eventSystem != null)
                eventSystem.SetActive(false);
        }
        #endregion methods
    }
}