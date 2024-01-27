using Core.Behaviors;
using Core.Scriptables;
using Core.Types;
using ModestTree;
using TMPro;
using UI.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Behaviors
{
    public class CoreMenu : BaseMenu
    {
        [SerializeField] private Image _nextBallTipIcon;
        [SerializeField] private TMP_Text _nextBallTipScore;
        [SerializeField] private TMP_Text _recordScore;
        [SerializeField] private TMP_Text _currentScore;

        [Inject] private BallSpawner _ballSpawner;
        [Inject] private BallsDataPackContainer _ballsDataPackContainer;
        
        public override MenuName Name => MenuName.CoreGame;

        private void Awake()
        {
            _ballSpawner.NewNextBallGenerated += OnNewNextBallGenerated;
        }

        private void OnDestroy()
        {
            _ballSpawner.NewNextBallGenerated -= OnNewNextBallGenerated;
        }
        
        private void OnNewNextBallGenerated(BallData nextBallData)
        {
            _nextBallTipIcon.color = nextBallData.Color;
            _nextBallTipIcon.sprite = nextBallData?.Sprite;
            
            var ballsDataPack = _ballsDataPackContainer.GetActivePack();
            var nextBallScore = ballsDataPack.Balls.IndexOf(nextBallData) * 2;
            if (nextBallScore == 0)
                nextBallScore = 2;
            
            _nextBallTipScore.text = nextBallScore.ToString();
        }
    }
}