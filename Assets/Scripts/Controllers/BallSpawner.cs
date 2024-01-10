using System.Collections;
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
        _currentBallData = null == _currentBallData ? _ballsData.Balls.GetRandomElement() : _nextBallData;
        _nextBallData = _ballsData.Balls.GetRandomElement();

        _currentPhysicBall = Instantiate(_ballPrefab, _ballSpawnParent);
        _currentPhysicBall.Initialize(_currentBallData.Size, _currentBallData.Score, _currentBallData.Color);

        _currentPhysicBall.MergeReady += _ballsMerger.MergeBalls;
        _currentPhysicBall.Rigidbody.isKinematic = true;

        _nextBallTipIcon.color = _nextBallData.Color;

        _nextBallTipScore.text = _nextBallData.Score.ToString();
    }
}
