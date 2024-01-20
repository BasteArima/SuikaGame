using System;
using UnityEngine;
using Zenject;

namespace Core.Behaviors
{
    public class PlayerBallDropController : MonoBehaviour
    {
        public event Action<Vector2> BallDropClicked;

        [Inject] private PlayerMoveController _playerMoveController;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                BallDropClicked?.Invoke(_playerMoveController.CurrentReadyToDropBallPosition);
        }
    }
}