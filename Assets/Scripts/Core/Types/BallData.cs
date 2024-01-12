using Sirenix.OdinInspector;
using Sound.Enums;
using UnityEngine;

namespace Core.Types
{
    [System.Serializable]
    public class BallData
    {
        public float Size;
        public int Score;
        public Sprite Sprite;
        public Color Color;
        public SoundIds MergeSound;
        public bool CanSpawn;
        [ShowIf(nameof(CanSpawn))] public float SpawnChance;
    }
}