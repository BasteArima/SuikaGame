using Core.Scriptables;
using UnityEngine;
using Zenject;

namespace Core.Behaviors
{
    public class BallsMerger : MonoBehaviour
    {
        private BallsDataPack _ballsDataPack;
        private BallSpawner _ballSpawner;

        [Inject]
        private void Construct(BallSpawner ballSpawner, BallsDataPackContainer ballsDataPackContainer)
        {
            _ballSpawner = ballSpawner;
            _ballsDataPack = ballsDataPackContainer.GetActivePack();
        }

        public void MergeBalls(PhysicBall ball1, PhysicBall ball2)
        {
            var nextLevelBall = _ballsDataPack.GetNextLevelBall(ball1.Size);
            if (null != nextLevelBall)
            {
                ball2.MergeReady -= MergeBalls;
                _ballSpawner.BallsPool.Destroy(ball2);
                ball1.Initialize(nextLevelBall.Size, ball1.Score * _ballsDataPack.ScoreMultiplier, nextLevelBall.Color,
                    nextLevelBall.Sprite);
            }
        }
    }
}