using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using EditorCustom.Attributes;
using UnityEngine.Events;
using Universal.Behaviour;
using Universal.Collections.Generic;
using Game.Serialization.Settings;
using Universal.Serialization;
using Universal.Collections;
using System.Collections.Generic;
using System.Collections;

namespace Game.Audio
{
    [System.Serializable]
    public class AudioManager : SavingDependentInjectable
    {
        #region fields & properties
        public static UnityAction OnAnyVolumeChanged;

        private static AudioManager Instance;
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.NullReferenceException"></exception>
        public static AudioClip CurrentQueueClip = null;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource ambienceSource;
        [SerializeField] private ObjectPool<DestroyablePoolableObject> oneShotClips;
        public static float NextSoundScale { get; set; } = 1f;
        private static float musicScale = 1f;

        [SerializeField] private List<AudioClip> menuMusic;

        [Title("Read Only")]
        [SerializeField][ReadOnly] private AudioListener activeAudioListener = null;
        private static Coroutine switchMusicCoroutine = null;
        #endregion fields & properties

        #region methods
        public override void Initialize()
        {
            Instance = this;
            base.Initialize();
            Universal.Serialization.AudioSettings audioSettings = SettingsData.Data.AudioSettings;
            audioSettings.MusicData.OnVolumeChanged += UpdateMusicVolumes;
            audioSettings.AudioData.OnVolumeChanged += UpdateMusicVolumes;
            audioSettings.SoundData.OnVolumeChanged += UpdateSoundVolume;
            SceneLoader.OnSceneLoaded += Awake;
            PlayMusicDependOnScene();
        }
        public override void Dispose()
        {
            base.Dispose();
            Universal.Serialization.AudioSettings audioSettings = SettingsData.Data.AudioSettings;
            audioSettings.MusicData.OnVolumeChanged -= UpdateMusicVolumes;
            audioSettings.AudioData.OnVolumeChanged -= UpdateMusicVolumes;
            audioSettings.SoundData.OnVolumeChanged -= UpdateSoundVolume;
            SceneLoader.OnSceneLoaded -= Awake;
        }

        public override void Awake()
        {
            base.Awake();
            FindActiveAudioListener();
            PlayMusicDependOnScene();
        }
        private void FindActiveAudioListener()
        {
            activeAudioListener = GameObject.FindObjectsByType<AudioListener>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(x => x.enabled).FirstOrDefault();
        }
        private void PlayMusicDependOnScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            switch (sceneName)
            {
                case "Menu": SwitchMusic(GetRandomClip(menuMusic)); break;
            }
        }
        private AudioClip GetRandomClip(List<AudioClip> clips) => clips[Random.Range(0, clips.Count)];
        /// <summary>
        /// Plays clip at audio listener
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="type"></param>
        public static void PlayClip(AudioClip clip, AudioType type)
        {
            PlayClipAtPoint(clip, type, Instance.activeAudioListener == null ? Camera.main.transform.position : Instance.activeAudioListener.transform.position);
        }
        /// <summary>
        /// Plays clip at audio listener
        /// </summary>
        public static void PlayClip(AudioClip clip, AudioType type, out AudioSource playableSource)
        {
            PlayClipAtPoint(clip, type, GetActiveAudioListener().position, out playableSource);
        }
        public static Transform GetActiveAudioListener() => Instance.activeAudioListener == null ? Camera.main.transform : Instance.activeAudioListener.transform;
        public static void PlayClipAtPoint(AudioClip clip, AudioType type, Vector3 position) => PlayClipAtPoint(clip, type, position, out _);
        public static void PlayClipAtPoint(AudioClip clip, AudioType type, Vector3 position, out AudioSource playableSource)
        {
            float volume = 1f * GetValueByType(type) * GetValueByType(AudioType.Audio) * NextSoundScale;
            NextSoundScale = 1f;
            PlayClipAtPoint(clip, position, volume, out playableSource);
        }
        private static void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume, out AudioSource audioSource)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                audioSource = null;
                return;
            }
#endif //UNITY_EDITOR
            PoolableAudioSource poolableAudioSource = (PoolableAudioSource)Instance.oneShotClips.GetObject();
            poolableAudioSource.Init(clip, position, volume);
            audioSource = poolableAudioSource.AudioSource;
        }
        public static void AddAmbient(AudioClip clip, bool force = false, float defaultMusicScale = 0.8f)
        {
            if (force || Instance.ambienceSource.clip != clip)
            {
                Instance.ambienceSource.clip = clip;
            }
            musicScale = defaultMusicScale;
            Instance.UpdateCurrentMusicVolume();
            Instance.UpdateAmbientVolume();
            Instance.ambienceSource.Play();
        }
        public static void RemoveAmbient()
        {
            musicScale = 1f;
            Instance.ambienceSource.clip = null;
            Instance.ambienceSource.Stop();
            Instance.UpdateCurrentMusicVolume();
        }
        public static void PlayMusic(AudioClip clip, [Optional] bool force)
        {
            if (force || Instance.musicSource.clip != clip)
            {
                Instance.musicSource.clip = clip;
                Instance.UpdateCurrentMusicVolume();
                Instance.musicSource.Play();
            }
        }
        public static void SwitchMusic(AudioClip clip, float timeToWait = 2, bool force = false)
        {
            if (force || Instance.musicSource.clip != clip)
            {
                CurrentQueueClip = clip;
                if (switchMusicCoroutine != null) SingleGameInstance.Instance.StopCoroutine(switchMusicCoroutine);
                switchMusicCoroutine = SingleGameInstance.Instance.StartCoroutine(SwitchMusicIEnumerator(timeToWait));
            }
        }
        private static IEnumerator SwitchMusicIEnumerator(float timeToWait)
        {
            float halfTime = timeToWait / 2f;
            float waited = 0f;
            while (waited < halfTime)
            {
                musicScale = Mathf.Lerp(1, 0, waited / halfTime);
                waited += Time.deltaTime;
                Instance.UpdateCurrentMusicVolume();
                yield return null;
            }
            
            Instance.musicSource.clip = CurrentQueueClip;
            Instance.musicSource.Play();
            
            waited = 0f;
            while (waited < halfTime)
            {
                musicScale = Mathf.Lerp(0, 1, waited / halfTime);
                waited += Time.deltaTime;
                Instance.UpdateCurrentMusicVolume();
                yield return null;
            }
        }
        private void UpdateMusicVolumes(float _)
        {
            UpdateCurrentMusicVolume();
            UpdateAmbientVolume();
            OnAnyVolumeChanged?.Invoke();
        }
        private void UpdateSoundVolume(float soundVolume)
        {
            OnAnyVolumeChanged?.Invoke();
        }
        public void UpdateCurrentMusicVolume()
        {
            UpdateVolume(musicSource, AudioType.Music);
        }
        public void UpdateAmbientVolume()
        {
            ambienceSource.volume = 0.5f * GetValueByType(AudioType.Music) * GetValueByType(AudioType.Audio);
        }
        public static float GetScaledVolume(AudioType type) => GetValueByType(type) * GetValueByType(AudioType.Audio);
        private static void UpdateVolume(AudioSource audioSource, AudioType type) => audioSource.volume = musicScale * GetScaledVolume(type);
        public static float GetValueByType(AudioType type) => (type) switch
        {
            AudioType.Music => SettingsData.Data.AudioSettings.MusicData.Volume,
            AudioType.Sound => SettingsData.Data.AudioSettings.SoundData.Volume,
            AudioType.Audio => SettingsData.Data.AudioSettings.AudioData.Volume,
            _ => throw new System.NotImplementedException()
        };
        #endregion methods
    }
}