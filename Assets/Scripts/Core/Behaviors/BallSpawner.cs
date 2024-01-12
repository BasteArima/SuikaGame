using System.Collections;
using Core.Scriptables;
using Core.Types;
using ModestTree;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Behaviors
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private PhysicBall _ballPrefab;
        [SerializeField] private Transform _ballSpawnParent;
        [SerializeField] private Transform _droppedBallParent;
        [SerializeField] private Image _nextBallTipIcon;
        [SerializeField] private TMP_Text _nextBallTipScore;
        [SerializeField] private float _waitBeforeSpawnNewBalls;

        private PlayerMoveController _playerMoveController;
        private BallsDataPack _ballsDataPack;
        private BallData _currentBallData;
        private BallData _nextBallData;
        private PhysicBall _currentPhysicBall;
        private bool _canCreateNewBall;

        public PoolServices<PhysicBall> BallsPool { get; private set; }

        [Inject]
        protected virtual void Construct(PlayerMoveController playerMoveController,
            BallsDataPackContainer ballsDataPackContainer)
        {
            _playerMoveController = playerMoveController;
            _ballsDataPack = ballsDataPackContainer.GetActivePack();

            MessageBroker.Default
                .Receive<StartGameSignal>()
                .Subscribe(OnGameStarted)
                .AddTo(gameObject);

            MessageBroker.Default
                .Receive<GameOverSignal>()
                .Subscribe(OnGameOver)
                .AddTo(gameObject);
        }

        private void Start()
        {
            MessageBroker.Default.Publish(new StartGameSignal());
        }

        private void OnGameStarted(StartGameSignal signal)
        {
            BallsPool = new PoolServices<PhysicBall>();
            _canCreateNewBall = true;
            _playerMoveController.BallDropClicked += OnBallDropClicked;
            GenerateNewBalls();
        }

        private void OnGameOver(GameOverSignal signal)
        {
            foreach (PhysicBall ball in BallsPool)
                BallsPool.Destroy(ball);
        }

        private void OnDestroy()
        {
            _playerMoveController.BallDropClicked -= OnBallDropClicked;
        }

        private void OnBallDropClicked(Vector2 ballDropPosition)
        {
            if (!_canCreateNewBall) return;
            _canCreateNewBall = false;
            _currentPhysicBall.transform.SetParent(_droppedBallParent);
            _currentPhysicBall.Rigidbody.isKinematic = false;
            StartCoroutine(CreateNewBallsWithPause());
        }

        private IEnumerator CreateNewBallsWithPause()
        {
            yield return new WaitForSeconds(_waitBeforeSpawnNewBalls);
            GenerateNewBalls();
            _canCreateNewBall = true;
        }

        private void GenerateNewBalls()
        {
            _currentBallData = null == _currentBallData ? _ballsDataPack.GetRandomBall() : _nextBallData;
            _nextBallData = _ballsDataPack.GetRandomBall();

            _currentPhysicBall = BallsPool.Create(_ballPrefab);
            _currentPhysicBall.transform.parent = _ballSpawnParent;
            _currentPhysicBall.transform.localPosition = Vector3.zero;

            _currentPhysicBall.Initialize(_currentBallData);

            _currentPhysicBall.Rigidbody.isKinematic = true;

            _nextBallTipIcon.color = _nextBallData.Color;
            if (null != _nextBallData.Sprite)
                _nextBallTipIcon.sprite = _nextBallData.Sprite;
            var nextBallScore = _ballsDataPack.Balls.IndexOf(_nextBallData) * 2;
            if (nextBallScore == 0)
                nextBallScore = 2;
            _nextBallTipScore.text = nextBallScore.ToString();
        }
    }
}