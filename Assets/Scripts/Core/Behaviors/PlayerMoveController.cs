using UnityEngine;

namespace Core.Behaviors
{
    public class PlayerMoveController : MonoBehaviour
    {

        [SerializeField] private Transform _playerSpawnParent;
        [SerializeField] private float _leftLimit;
        [SerializeField] private float _rightLimit;

        public Vector2 CurrentReadyToDropBallPosition { get; private set; }
        
        private void Update()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CurrentReadyToDropBallPosition = new Vector2(mousePosition.x, _playerSpawnParent.position.y);
            if (mousePosition.x > _leftLimit && mousePosition.x < _rightLimit)
                _playerSpawnParent.position = CurrentReadyToDropBallPosition;
        }
    }
}