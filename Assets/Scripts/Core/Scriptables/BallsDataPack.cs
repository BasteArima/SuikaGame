using System.Linq;
using ModestTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Scriptables
{
    [System.Serializable]
    public class BallData
    {
        public float Size;
        public Sprite Sprite;
        public Color Color;
        public bool CanSpawn;
    }

    [CreateAssetMenu(menuName = "Data/BallsData")]
    public class BallsDataPack : SerializedScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private BallData[] _balls;
        [SerializeField] private int _scoreMultiplier = 2;

        public string Id => _id;
        public BallData[] Balls => _balls;
        public int ScoreMultiplier => _scoreMultiplier;
        [field: SerializeField] public bool ActivePack { get; set; }

        public BallData GetNextLevelBall(float size)
        {
            var currentBallData = _balls.ToList().Find(x => x.Size == size);
            var nextLevelBallIndex = _balls.IndexOf(currentBallData) + 1;
            return nextLevelBallIndex < _balls.Length ? _balls[nextLevelBallIndex] : null;
        }

        public BallData GetRandomBall()
        {
            var random = Random.Range(0, _balls.Count(x => x.CanSpawn));
            return _balls[random];
        }

        [Button]
        private void ClearAllColors()
        {
            foreach (var ball in _balls)
                ball.Color = Color.white;
        }
    }
}