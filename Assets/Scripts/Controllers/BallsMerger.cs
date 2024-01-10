using Scriptables;
using UnityEngine;
using Zenject;

public class BallsMerger : MonoBehaviour
{
    [Inject] private BallsData _ballsData;

    public void MergeBalls(PhysicBall ball1, PhysicBall ball2)
    {
        var nextLevelBall = _ballsData.GetNextLevelBall(ball1.Size);
        if(null != nextLevelBall)
        {
            ball2.MergeReady -= MergeBalls;
            Destroy(ball2.gameObject);
            ball1.Initialize(nextLevelBall.Size, nextLevelBall.Score, nextLevelBall.Color);
        }
    }
}
