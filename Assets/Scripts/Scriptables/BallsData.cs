using System.Linq;
using ModestTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scriptables
{
    [System.Serializable]
    public class BallData
    {
        public float Size;
        public int Score;
        public Color Color;
    }
    
    [CreateAssetMenu(menuName = "Data/BallsData")]
    public class BallsData : SerializedScriptableObject
    {
        [SerializeField] private BallData[] _balls;

        public BallData[] Balls => _balls;

        public BallData GetNextLevelBall(float Size)
        {
            var currentBallData = _balls.ToList().Find(x => x.Size == Size);
            var nextLevelBallIndex = _balls.IndexOf(currentBallData) + 1;
            Debug.Log($"nextLevelBallIndex: {nextLevelBallIndex}");
            Debug.Log($"nextLevelBallIndex < _balls.Length - 1: {nextLevelBallIndex < _balls.Length}");
            if (nextLevelBallIndex < _balls.Length)
                return _balls[nextLevelBallIndex];
            else
                return null;
        }
    }
}