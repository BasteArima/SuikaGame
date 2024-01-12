using Core.Scriptables;
using Core.Signals;
using Sound.Behaviors;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.Behaviors
{
    public class BallsMerger : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        
        private BallsDataPack _ballsDataPack;
        private BallSpawner _ballSpawner;

        private int _score;
        
        [Inject]
        private void Construct(BallSpawner ballSpawner, BallsDataPackContainer ballsDataPackContainer)
        {
            _ballSpawner = ballSpawner;
            _ballsDataPack = ballsDataPackContainer.GetActivePack();
            MessageBroker.Default
                .Receive<MergeBallsSignal>()
                .Subscribe(OnMergeBalls)
                .AddTo(gameObject);
        }

        private void OnMergeBalls(MergeBallsSignal signal)
        {
            var nextLevelBall = _ballsDataPack.GetNextLevelBall(signal.FirstBall.BallData.Size);
            
            if (null == nextLevelBall) return;

            SoundDesigner.PlaySound(signal.FirstBall.MergeSound);
            _ballSpawner.BallsPool.Destroy(signal.SecondBall);
            signal.FirstBall.Initialize(nextLevelBall);
            
            _score += signal.FirstBall.Score;
            _scoreText.text = _score.ToString();
        }
    }
}