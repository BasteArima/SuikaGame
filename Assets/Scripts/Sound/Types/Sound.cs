using Sound.Enums;
using UnityEngine;

namespace Sound.Types
{
    [System.Serializable]
    public class Sound
    {
        public SoundIds Id;
        public SoundBaseType BaseType;
        public AudioClip AudioClip;
        public AudioSource AudioSource;
        [Range(0f, 1f)] public float Volume = 1;
        public float Pitch = 1;
        public bool IsLoop;
        public float PitchRange;
        public bool PlayOnAwake;
    }
}