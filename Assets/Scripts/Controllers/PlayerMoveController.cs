using System;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public event Action<Vector2> BallDropClicked;
    
    [SerializeField] private Transform _playerSpawnParent;
    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;

    private void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var newPlayerSpawnParentPosition = new Vector2(mousePosition.x, _playerSpawnParent.position.y);
        if(mousePosition.x > _leftLimit && mousePosition.x < _rightLimit )
            _playerSpawnParent.position = newPlayerSpawnParentPosition;
        
        if (Input.GetMouseButtonDown(0))
            BallDropClicked?.Invoke(newPlayerSpawnParentPosition);
    }
}
