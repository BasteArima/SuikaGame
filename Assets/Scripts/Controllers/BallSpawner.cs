using System.Collections;
using ModestTree;
using Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private PhysicBall _ballPrefab;
    [SerializeField] private Transform _ballSpawnParent;
    [SerializeField] private Image _nextBallTipIcon;
    [SerializeField] private TMP_Text _nextBallTipScore;
    [SerializeField] private float _waitBeforeSpawnNewBalls;
 
    [Inject] private BallsData _ballsData;
    [Inject] private PlayerMoveController _playerMoveController;
    [Inject] private BallsMerger _ballsMerger;
    
    private BallData _currentBallData;
    private BallData _nextBallData;
    private PhysicBall _currentPhysicBall;

    private void Start()
    {
        _playerMoveController.BallDropClicked += OnBallDropClicked;
        GenerateNewBalls();
    }

    private void OnDestroy()
    {
        _playerMoveController.BallDropClicked -= OnBallDropClicked;
    }

    private void OnBallDropClicked(Vector2 ballDropPosition)
    {
        _currentPhysicBall.transform.SetParent(null);
        _currentPhysicBall.Rigidbody.isKinematic = false;
        StartCoroutine(CreateNewBallsWithPause());
    }

    private IEnumerator CreateNewBallsWithPause()
    {
        yield return new WaitForSeconds(_waitBeforeSpawnNewBalls);
        GenerateNewBalls();
    }

    private void GenerateNewBalls()
    {
        _currentBallData = null == _currentBallData ? _ballsData.GetRandomBall() : _nextBallData;
        _nextBallData = _ballsData.GetRandomBall();

        _currentPhysicBall = Instantiate(_ballPrefab, _ballSpawnParent);
        var ballScore = _ballsData.Balls.IndexOf(_currentBallData) * 2;
        if (ballScore == 0)
            ballScore = _ballsData.ScoreMultiplier;
        _currentPhysicBall.Initialize(_currentBallData.Size, ballScore, _currentBallData.Color);

        _currentPhysicBall.MergeReady += _ballsMerger.MergeBalls;
        _currentPhysicBall.Rigidbody.isKinematic = true;

        _nextBallTipIcon.color = _nextBallData.Color;
        var nextBallScore = _ballsData.Balls.IndexOf(_nextBallData) * 2;
        if (nextBallScore == 0)
            nextBallScore = 2;
        _nextBallTipScore.text = nextBallScore.ToString();
    }
}
