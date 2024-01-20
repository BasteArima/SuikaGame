using UnityEngine;

namespace Core.Behaviors
{
    public class ScoreController : MonoBehaviour
    {
        private const string SCORE_RECORD_KEY = "ScoreRecord";

        private int _recordScore;
        private int _currentScore;

        private void Awake()
        {
            
        }
    }
}