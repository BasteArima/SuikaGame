using System;
using System.Collections;
using Core.Scriptables;
using Core.Types;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Behaviors
{
    public class BallSpawner : MonoBehaviour
    {
        public event Action<BallData> NewCurrentBallGenerated;
        public event Action<BallData> NewNextBallGenerated;
        
        [SerializeField] private PhysicBall _ballPrefab;
        [SerializeField] private Transform _ballSpawnParent;
        [SerializeField] private Transform _droppedBallParent;
        [SerializeField] private float _waitBeforeSpawnNewBalls;
        [SerializeField] private SpriteRenderer _ballFallTip;

        private PlayerBallDropController _playerBallDropController;
        private BallsDataPack _ballsDataPack;
        private BallData _currentBallData;
        private BallData _nextBallData;
        private PhysicBall _currentPhysicBall;
        private bool _canCreateNewBall;

        public PoolServices<PhysicBall> BallsPool { get; private set; }

        [Inject]
        protected virtual void Construct(PlayerBallDropController playerBallDropController,
            BallsDataPackContainer ballsDataPackContainer)
        {
            _playerBallDropController = playerBallDropController;
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
            _playerBallDropController.BallDropClicked += OnBallDropClicked;
            GenerateNewCurrentBall();
            GenerateNewNextBall();
            CreateNewPhysicBall();
            _ballFallTip.DOFade(0.5f, _waitBeforeSpawnNewBalls / 3);
        }

        private void OnGameOver(GameOverSignal signal)
        {
            foreach (PhysicBall ball in BallsPool)
                BallsPool.Destroy(ball);
        }

        private void OnDestroy()
        {
            _playerBallDropController.BallDropClicked -= OnBallDropClicked;
        }

        private void OnBallDropClicked(Vector2 ballDropPosition)
        {
            if (!_canCreateNewBall) return;
            _ballFallTip.DOFade(0.1f, _waitBeforeSpawnNewBalls / 3);
            _canCreateNewBall = false;
            _currentPhysicBall.transform.SetParent(_droppedBallParent);
            _currentPhysicBall.Rigidbody.isKinematic = false;
            StartCoroutine(CreateNewBallsWithPause());
        }

        private IEnumerator CreateNewBallsWithPause()
        {
            yield return new WaitForSeconds(_waitBeforeSpawnNewBalls);
            GenerateNewCurrentBall();
            GenerateNewNextBall();
            CreateNewPhysicBall();
            _ballFallTip.DOFade(0.5f, _waitBeforeSpawnNewBalls / 3);
            _canCreateNewBall = true;
        }

        private void GenerateNewCurrentBall()
        {
            _currentBallData = null == _currentBallData ? _ballsDataPack.GetRandomBall() : _nextBallData;
            NewCurrentBallGenerated?.Invoke(_currentBallData);
        }

        private void CreateNewPhysicBall()
        {
            _currentPhysicBall = BallsPool.Create(_ballPrefab);
            _currentPhysicBall.transform.parent = _ballSpawnParent;
            _currentPhysicBall.transform.localPosition = Vector3.zero;

            _currentPhysicBall.Initialize(_currentBallData);
            _currentPhysicBall.Rigidbody.isKinematic = true;
        }
        
        public void GenerateNewNextBall()
        {
            _nextBallData = _ballsDataPack.GetRandomBall();
            NewNextBallGenerated?.Invoke(_nextBallData);
        }
    }
}