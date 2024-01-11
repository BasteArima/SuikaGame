using System.Collections.Generic;
using System.Linq;
using Sound.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound.Behaviors
{
    public class SoundDesigner : MonoBehaviour
    {
        private static SoundDesigner _instance;

        [SerializeField, Range(0, 1)] private float _defaultSoundVolume = 0.5f;
        [SerializeField, Range(0, 1)] private float _defaultMusicVolume = 0.25f;

        public static float GlobalSoundVolume { get; set; }
        public static float GlobalMusicVolume { get; set; }
        public static bool SoundMuted { get; set; }
        public static bool MusicMuted { get; set; }

        public List<Sound.Types.Sound> sounds;

        private void Awake()
        {
            if (_instance != null) Destroy(_instance.gameObject);
            _instance = this;

            foreach (var sound in sounds)
            {
                var newAudioSource = gameObject.AddComponent<AudioSource>();
                newAudioSource.clip = sound.AudioClip;
                newAudioSource.loop = sound.IsLoop;
                newAudioSource.playOnAwake = sound.PlayOnAwake;
                newAudioSource.pitch = sound.Pitch;
                newAudioSource.volume = sound.Volume;
                sound.AudioSource = newAudioSource;
            }

            GlobalSoundVolume = PlayerPrefs.GetFloat("SoundValue", _defaultSoundVolume);
            GlobalMusicVolume = PlayerPrefs.GetFloat("MusicValue", _defaultMusicVolume);
        }

        private void Start()
        {
            SetMuteByBaseType(SoundBaseType.Sound, SoundMuted);
            SetMuteByBaseType(SoundBaseType.Music, MusicMuted);
        }

        /// <summary>
        /// Проигрывет звук данного типа
        /// </summary>
        /// <param name="id">Тип звука</param>
        public static void PlaySound(SoundIds id)
        {
            var sound = _instance.sounds.SingleOrDefault(s => s.Id == id);
            if (sound == null) return;
            sound.AudioSource.pitch += Random.Range(-sound.PitchRange, sound.PitchRange);
            sound.AudioSource.Play();
            sound.AudioSource.pitch = sound.Pitch;
        }

        /// <summary>
        /// Останавливает проигрывание звука данного типа
        /// </summary>
        /// <param name="id"></param>
        public static void StopSound(SoundIds id)
        {
            var sound = _instance.sounds.SingleOrDefault(s => s.Id == id);
            if (sound == null) return;
            sound.AudioSource.Stop();
        }

        /// <summary>
        /// Мьютит заданный звук
        /// </summary>
        /// <param name="id"></param>
        public static void MuteSound(SoundIds id, bool mute)
        {
            var sound = _instance.sounds.SingleOrDefault(s => s.Id == id);
            if (sound == null) return;
            sound.AudioSource.mute = mute;
        }

        /// <summary>
        /// Проигрывет звуки данного типа
        /// </summary>
        /// <param name="id">Базовый тип звука</param>
        public static void PlayMultipleSound(SoundIds id)
        {
            foreach (var sound in _instance.sounds.Where(s => s.Id == id))
                sound.AudioSource.Play();
        }

        /// <summary>
        /// Влючает/выключает audio source привязанный к типу
        /// </summary>
        /// <param name="id">Тип звука к которому привязан audio source</param>
        /// <param name="state">Значение активности (enabled) </param>
        public static void SetActiveAudioSource(SoundIds id, bool state)
        {
            var sound = _instance.sounds.SingleOrDefault(s => s.Id == id);
            if (sound == null) return;
            sound.AudioSource.enabled = state;
        }

        public static void SetVolumeAudioSource(SoundBaseType type, float volume)
        {
            foreach (var sound in _instance.sounds.Where(s => s.BaseType == type))
            {
                sound.AudioSource.volume = volume;
            }
        }

        public static void SetAllMuteAudioSource(bool state)
        {
            foreach (var sound in _instance.sounds)
                sound.AudioSource.mute = state;
        }

        public static void SetMuteByBaseType(SoundBaseType type, bool state)
        {
            foreach (var sound in _instance.sounds.Where(s => s.BaseType == type))
                sound.AudioSource.mute = state;
        }
    }
}