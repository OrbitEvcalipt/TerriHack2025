using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunnyBlox.Utils
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;
        [SerializeField] private AudioDatabase sfxDatabase;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource sfxSourceSimple;
        [SerializeField] private AudioSource musicSource;

        void Start()
        {
            instance = this;
            if (PlayerPrefs.HasKey("MusicEnable"))
            {
                bool music = PlayerPrefs.GetInt("MusicEnable") == 1 ? true : false;
                musicSource.enabled = music;
            }
            else
                PlayerPrefs.SetInt("MusicEnable", 1);

            sfxSourceSimple.enabled = sfxSource.enabled = Settings.SoundIsActive;
        }

        /// <summary>
        /// Base audio manager play method
        /// </summary>
        /// <param name="clipName"> Audio clip name </param>
        /// <param name="volume"> Audio clip volume </param>
        /// <param name="randomPitch"> If "true", generate taller random pitch for playing audio clip </param>
        /// <param name="lowerPitch"> If "true", generate lower random pitch for playing audio clip </param>
        /// 
        public static void PlayOneShotSFX(AudioDatabaseEnum clipName, bool randomPitch = false,
            bool differentDelay = false)
        {
            instance.pitchValue = 0.8f;
            instance.sfxSourceSimple.pitch = 1f;

            SetPitch(randomPitch);

            if (differentDelay)
                instance.StartCoroutine(instance.DelayAndPlay(clipName, randomPitch));
            else
                instance.sfxSourceSimple.PlayOneShot(instance.sfxDatabase.audioDatabase[clipName.ToString()], 1);
        }

        private static bool canPlayByWave = true;

        public static void PlayByWaveOneShotSFX(AudioDatabaseEnum clipName, bool randomPitch = false,
            bool differentDelay = false)
        {
            SetPitch(randomPitch);
            if (canPlayByWave)
            {
                if (differentDelay)
                    instance.StartCoroutine(instance.DelayAndPlay(clipName, randomPitch));
                else
                    instance.sfxSource.PlayOneShot(instance.sfxDatabase.audioDatabase[clipName.ToString()], 1);
                instance.StartCoroutine(instance.LockWaveSFX());
            }
        }

        float pitchValue = 0.8f;
        float resetTimer = 0.5f;
        private Coroutine resetPich;

        public static void PlayByWaveOneShotSFX(AudioDatabaseEnum clipName, bool increasePitch)
        {
            //SetPitch(randomPitch);
            if (increasePitch)
            {
                instance.sfxSource.pitch = instance.pitchValue;
                instance.pitchValue += 0.01f;
                if (instance.pitchValue >= 2)
                    instance.pitchValue = 2f;
            }

            if (canPlayByWave)
            {
                instance.sfxSource.PlayOneShot(instance.sfxDatabase.audioDatabase[clipName.ToString()], 1);
                instance.StartCoroutine(instance.LockWaveSFX());
            }

            if (instance.resetPich != null)
                instance.resetTimer = 0.5f;
            else
            {
                instance.resetTimer = 0.5f;
                instance.resetPich = instance.StartCoroutine(instance.ResetPitch());
            }
        }

        private IEnumerator LockWaveSFX()
        {
            canPlayByWave = false;
            yield return new WaitForSecondsRealtime(0.02f);
            canPlayByWave = true;
        }

        private IEnumerator ResetPitch()
        {
            while (resetTimer > 0)
            {
                yield return new WaitForSecondsRealtime(0.1f);
                resetTimer -= 0.1f;
            }

            instance.pitchValue = 0.8f;
            instance.sfxSource.pitch = instance.pitchValue;
            instance.resetPich = null;
        }

        IEnumerator DelayAndPlay(AudioDatabaseEnum clipName, bool randomPitch = false)
        {
            yield return new WaitForSeconds(Random.Range(0.05f, 0.7f));
            SetPitch(randomPitch);
            instance.sfxSource.PlayOneShot(instance.sfxDatabase.audioDatabase[clipName.ToString()]);
        }

        private static void SetPitch(bool randomPitch)
        {
            if (randomPitch)
            {
                float randomPitchValue = Random.Range(0.8f, 2f);
                instance.sfxSource.pitch = randomPitchValue;
                instance.sfxSourceSimple.pitch = randomPitchValue;
            }
            else
            {
                instance.sfxSource.pitch = 1f;
                instance.sfxSourceSimple.pitch = 1f;
            }
        }

        public static void SetSoundSettings(bool enabled)
        {
            instance.sfxSource.enabled = instance.sfxSourceSimple.enabled = enabled;
        }
    }
}